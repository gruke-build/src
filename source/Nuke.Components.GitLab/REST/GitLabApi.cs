// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NGitLab;
using NGitLab.Models;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.Tools.GitLab;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.Utilities.Net;
using Serilog;

namespace Nuke.Components.GitLab;

[PublicAPI]
public static class GitLabApi
{
    public static IHttpClientProxy CreateHttpClient(string host, string accessToken, TimeSpan? timeout = null)
    {
        return new HttpClientProxy(new HttpClient
                                   {
                                       Timeout = timeout ?? TimeSpan.FromSeconds(100),
                                       BaseAddress = new Uri(host.EnsureEnding("/api/v4/").EnsureStarting("https://")),
                                       DefaultRequestHeaders =
                                       {
                                           UserAgent = { new ProductInfoHeaderValue("gruke", "1.0.0") },
                                           Authorization = AuthenticationHeaderValue.Parse($"Bearer {accessToken}")
                                       }
                                   },
            (fmt, args, extraLines, _)
                =>
            {
                Log.Information(
                    args.Length is 0
                        ? fmt
                        : string.Format(fmt, args)
                );
                extraLines.ForEach(new Action<string>(Log.Information));
            }
        );
    }

    /// <summary>
    ///     Uploads a generic package to a specific project's Package Registry.
    /// </summary>
    /// <param name="http">The HTTP client to use.</param>
    /// <param name="uploads">The paths of the files to upload.</param>
    /// <param name="projectPath">The 'namespace/subpath' of the project.</param>
    /// <param name="packageName">The name of the generic package.</param>
    /// <param name="packageVersion">The version currently being uploaded to.</param>
    /// <returns>A <see cref="IReadOnlyList{string}"/> of errors that occurred when uploading. If this is empty, everything succeeded.</returns>
    [ItemCanBeNull]
    public static async Task<IReadOnlyList<string>> UploadGenericPackagesToSpecificProjectAsync(
        IHttpClientProxy http,
        IEnumerable<AbsolutePath> uploads,
        string projectPath,
        string packageName,
        string packageVersion
    )
    {
        List<string> errors = [];

        foreach (var upload in uploads)
        {
            try
            {
                HttpResponseMessage response;

                await using (var fileStream = File.OpenRead(upload))
                {
                    response = await http.PutAsync(
                        $"projects/{WebUtility.UrlEncode(projectPath)}/packages/generic/{packageName}/{packageVersion}/{
                            upload.Name.Replace(oldChar: ' ', newChar: '_') //GitLab errors when the file path name here has spaces in the name
                        }",
                        new StreamContent(fileStream)
                    );
                }

                if (response is null)
                {
                    errors.Add($"{upload}: Response object was not returned.");
                    continue;
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.NotFound:
                        return ["Invalid authorization."];
                    case HttpStatusCode.Forbidden:
                        return [$"Project {projectPath.SingleQuoteIfNeeded()} has the package registry disabled."];
                    default:
                        if (!response.IsSuccessStatusCode)
                            errors.Add(response.ReasonPhrase);
                        continue;
                }
            }
            catch (TaskCanceledException)
            {
                errors.Add($"Timed out uploading '{upload}'.");
            }
            catch (Exception e)
            {
                errors.Add($"Errored uploading '{upload}': {e.Message}");
            }
        }

        return errors;
    }

    /// <summary>
    ///     Uploads a generic package to a specific project's Package Registry.
    /// </summary>
    /// <param name="http">The HTTP client to use.</param>
    /// <param name="upload">The path of the file to upload.</param>
    /// <param name="projectPath">The 'namespace/subpath' of the project.</param>
    /// <param name="packageName">The name of the generic package.</param>
    /// <param name="packageVersion">The version currently being uploaded to.</param>
    /// <returns>null if HTTP 2xx is returned; an error string otherwise.</returns>
    [ItemCanBeNull]
    public static async Task<string> UploadGenericPackageToSpecificProjectAsync(
        IHttpClientProxy http,
        AbsolutePath upload,
        string projectPath,
        string packageName,
        string packageVersion
    )
    {
        try
        {
            HttpResponseMessage response;

            await using (var fileStream = File.OpenRead(upload))
            {
                response = await http.PutAsync(
                    $"projects/{WebUtility.UrlEncode(projectPath)}/packages/generic/{packageName}/{packageVersion}/{upload.Name}",
                    new StreamContent(fileStream)
                );
            }

            if (response is null)
                return "Response object was not returned.";

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:
                    return "Invalid authorization.";
                case HttpStatusCode.Forbidden:
                    return $"Project {projectPath.SingleQuoteIfNeeded()} has the package registry disabled.";
                default:
                    return response.IsSuccessStatusCode ? null : response.ReasonPhrase;
            }
        }
        catch (TaskCanceledException)
        {
            return $"Timed out uploading '{upload}'.";
        }
        catch (Exception e)
        {
            return $"Errored uploading '{upload}': {e.Message}";
        }
    }

    /// <summary>
    ///     Uploads a generic package to a specific project's Package Registry.
    /// </summary>
    /// <param name="http">The HTTP client to use.</param>
    /// <param name="upload">The path of the file to upload.</param>
    /// <param name="projectPath">The 'namespace/subpath' of the project.</param>
    /// <param name="packageName">The name of the generic package.</param>
    /// <param name="packageVersion">The version currently being uploaded to.</param>
    /// <param name="retries">The amount of times to retry if the upload fails.</param>
    /// <returns>null if HTTP 2xx is returned; an error string otherwise.</returns>
    [ItemCanBeNull]
    public static async Task<string> UploadGenericPackageToSpecificProjectWithRetryAsync(
        IHttpClientProxy http,
        AbsolutePath upload,
        string projectPath,
        string packageName,
        string packageVersion,
        uint retries = 2
    )
    {
        Retry:
        if (await UploadGenericPackageToSpecificProjectAsync(http,
                upload.NotNull(),
                projectPath.NotNull(),
                packageName.NotNull(),
                packageVersion.NotNull()
            ) is { } error)
        {
            if (retries > 0)
            {
                Log.Error(error);
                retries--;
                goto Retry;
            }

            return error;
        }

        Log.Information("Uploaded '{upload}' to the package registry on project '{glProjectPath}'.",
            upload, projectPath);
        return null;
    }

    /// <summary>
    ///     Uploads generic packages to a specific project's Package Registry.
    /// </summary>
    /// <param name="http">The HTTP client to use.</param>
    /// <param name="uploads">The paths of the files to upload.</param>
    /// <param name="projectPath">The 'namespace/subpath' of the project.</param>
    /// <param name="packageName">The name of the generic package.</param>
    /// <param name="packageVersion">The version currently being uploaded to.</param>
    /// <param name="retries">The amount of times to retry if the upload fails.</param>
    /// <returns>null if HTTP 2xx is returned; an error string otherwise.</returns>
    [ItemCanBeNull]
    public static async Task<IReadOnlyList<string>> UploadGenericPackagesToSpecificProjectWithRetryAsync(
        IHttpClientProxy http,
        IEnumerable<AbsolutePath> uploads,
        string projectPath,
        string packageName,
        string packageVersion,
        uint retries = 2
    )
    {
        List<string> errors = [];
        foreach (var upload in uploads)
        {
            var tries = retries;

            Retry:
            if (await UploadGenericPackageToSpecificProjectAsync(http,
                    upload.NotNull(),
                    projectPath.NotNull(),
                    packageName.NotNull(),
                    packageVersion.NotNull()
                ) is { } error)
            {
                if (tries > 0)
                {
                    Log.Error(error);
                    tries--;
                    goto Retry;
                }

                errors.Add(error);
                continue;
            }

            Log.Information("Uploaded '{upload}' to the package registry on project '{projectPath}'.", upload, projectPath);
        }

        return errors;
    }

    /// <summary>
    ///     Uploads a generic package to the current project's Package Registry.
    ///     <br/> <br/>
    ///     This method is expected to be executed in a CI environment, as it depends on:
    ///         <list type="number">
    ///             <item>
    ///                 <see cref="Common.CI.GitLab.GitLab.ProjectPath"/>
    ///             </item>
    ///         </list>
    /// </summary>
    /// <param name="gl">The current GitLab CI environment.</param>
    /// <param name="http">The HTTP client to use.</param>
    /// <param name="packageName">The name of the generic package.</param>
    /// <param name="packageVersion">The version currently being uploaded to.</param>
    /// <param name="upload">The path of the file to upload.</param>
    /// <param name="retries">The amount of retries to attempt if upload fails.</param>
    /// <returns>true if HTTP 2xx is returned; false otherwise.</returns>
    [ItemCanBeNull]
    public static async Task<string> UploadGenericPackageAsync(
        this Common.CI.GitLab.GitLab gl,
        IHttpClientProxy http,
        AbsolutePath upload,
        string packageName,
        string packageVersion,
        uint retries = 2)
    {
        Retry:
        if (await UploadGenericPackageToSpecificProjectAsync(
                http,
                upload.NotNull(),
                gl.ProjectPath.NotNull(),
                packageName.NotNull(),
                packageVersion.NotNull()
            ) is { } error && retries > 0)
        {
            Log.Error(error);
            retries--;
            goto Retry;
        }

        Log.Information("Uploaded '{upload}' to the package registry on project '{glProjectPath}'.",
            upload, gl.ProjectPath);
        return null;
    }

    /// <summary>
    ///     Uploads generic packages to the current project's Package Registry.
    ///     <br/> <br/>
    ///     This method is expected to be executed in a CI environment, as it depends on:
    ///         <list type="number">
    ///             <item>
    ///                 <see cref="Common.CI.GitLab.GitLab.ProjectPath"/>
    ///             </item>
    ///         </list>
    /// </summary>
    /// <param name="gl">The current GitLab CI environment.</param>
    /// <param name="http">The HTTP client to use.</param>
    /// <param name="packageName">The name of the generic package.</param>
    /// <param name="packageVersion">The version currently being uploaded to.</param>
    /// <param name="uploads">The paths of the files to upload.</param>
    /// <param name="retries">The amount of retries to attempt if upload fails for a file. This value is for EACH file, not for all files.</param>
    /// <returns>true if HTTP 2xx is returned for any upload; false otherwise.</returns>
    [ItemCanBeNull]
    public static async Task<IReadOnlyList<string>> UploadGenericPackagesAsync(
        this Common.CI.GitLab.GitLab gl,
        IHttpClientProxy http,
        IEnumerable<AbsolutePath> uploads,
        string packageName,
        string packageVersion,
        uint retries = 2)
    {
        List<string> errors = [];
        foreach (var upload in uploads)
        {
            var tries = retries;

            Retry:
            if (await UploadGenericPackageToSpecificProjectAsync(http,
                    upload.NotNull(),
                    gl.ProjectPath.NotNull(),
                    packageName.NotNull(),
                    packageVersion.NotNull()
                ) is { } error)
            {
                if (tries > 0)
                {
                    Log.Error(error);
                    tries--;
                    goto Retry;
                }

                errors.Add(error);
                continue;
            }

            Log.Information("Uploaded '{upload}' to the package registry on project '{glProjectPath}'.",
                upload, gl.ProjectPath);
        }

        return errors;
    }

    public static async Task UpdateReleaseAsync(
        string projectPath,
        string name,
        string releaseBody,
        string version,
        string @ref,
        ReleaseLink[] releaseLinks,
        GitRepository gr = null)
    {
        try
        {
            await GitLabTasks.ApiClient.GetReleases(projectPath)
                .CreateAsync(new ReleaseCreate
                             {
                                 TagName = version,
                                 Name = name ?? version,
                                 Assets = new ReleaseAssetsInfo { Links = releaseLinks, Count = releaseLinks.Length },
                                 Description = releaseBody,
                                 Ref = @ref ?? gr?.Branch
                             });
        }
        catch (GitLabException gle) when (gle.StatusCode is HttpStatusCode.Forbidden)
        {
            Assert.Fail("Project does not have releases enabled.");
        }
        catch (GitLabException gle) when (gle.StatusCode is HttpStatusCode.Conflict or HttpStatusCode.UnprocessableEntity)
        {
            var rc = GitLabTasks.ApiClient.GetReleases(projectPath);
            var currentReleaseLinks = GetCurrentReleaseLinks(rc, version);
            var rewritten = RewriteReleaseLinks(currentReleaseLinks, releaseLinks);

            var rlc = rc.ReleaseLinks(version);

            foreach (var existingLink in rewritten.ToUpdate)
            {
                // ReSharper disable once PossibleInvalidOperationException
                rlc.Update(existingLink.Id.Value, new ReleaseLinkUpdate
                                                  {
                                                      Url = existingLink.Url,
                                                      Name = existingLink.Name,
                                                      LinkType = existingLink.LinkType
                                                  });
            }

            foreach (var existingLink in rewritten.ToCreate)
            {
                rlc.Create(new ReleaseLinkCreate
                           {
                               Url = existingLink.Url,
                               Name = existingLink.Name,
                               LinkType = existingLink.LinkType
                           });
            }

            foreach (var existingLink in rewritten.ToDelete)
            {
                // ReSharper disable once PossibleInvalidOperationException
                rlc.Delete(existingLink.Id.Value);
            }
        }
    }

    private static (ReleaseLink[] ToUpdate, ReleaseLink[] ToCreate, ReleaseLink[] ToDelete) RewriteReleaseLinks(
        ReleaseLink[] source,
        ReleaseLink[] @new)
    {
        if (source.Length == @new.Length)
        {
            foreach (var (index, link) in @new.Index())
            {
                source[index].Name = link.Name;
                source[index].External = link.External;
                source[index].Url = link.Url;
                source[index].DirectAssetUrl = link.DirectAssetUrl;
                source[index].LinkType = link.LinkType;
            }

            return (source, [], []);
        }

        if (source.Length > @new.Length)
        {
            foreach (var (index, link) in @new.Index())
            {
                source[index].Name = link.Name;
                source[index].External = link.External;
                source[index].Url = link.Url;
                source[index].DirectAssetUrl = link.DirectAssetUrl;
                source[index].LinkType = link.LinkType;
            }

            var toUpdate = source.Take(@new.Length);
            var toDelete = source.TakeLast(source.Length - @new.Length);

            return (toUpdate.ToArray(), [], toDelete.ToArray());
        }

        if (source.Length < @new.Length)
        {
            Array.Resize(ref source, @new.Length);
            foreach (var (i, link) in @new.Index())
            {
                source[i] ??= new ReleaseLink();

                source[i].Name = link.Name;
                source[i].External = link.External;
                source[i].Url = link.Url;
                source[i].DirectAssetUrl = link.DirectAssetUrl;
                source[i].LinkType = link.LinkType;
            }

            return (source.Where(x => x.Id is not null).ToArray(), source.Where(x => x.Id is null).ToArray(), []);
        }

        throw new NotImplementedException();
    }

    private static ReleaseLink[] GetCurrentReleaseLinks(
        IReleaseClient releaseClient,
        string tagName)
    {
        return releaseClient[tagName].Assets.Links;
    }

    public static Task<GetProjectPackagesItem> FindMatchingPackageAsync(
        IHttpClientProxy http,
        string projectPath,
        string name,
        string version)
    {
        var p = PaginatedEndpoint<GetProjectPackagesItem>.Builder(http)
            .WithBaseUrl($"projects/{WebUtility.UrlEncode(projectPath)}/packages")
            .WithJsonContentParser(GitLabSerializerContexts.Default.IEnumerableGetProjectPackagesItem)
            .WithPerPageCount(100)
            .WithQueryStringParameters(
                QueryParameters.Sort("desc"),
                QueryParameters.OrderBy("created_at"),
                ("package_type", "generic")
            ).Build();

        return p.FindOneAsync(
            predicate: it => it.Name == name && it.Version == version,
            onNonSuccess: _ => Log.Error("Target project has the package registry disabled.")
        );
    }
}
