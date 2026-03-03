// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common.IO;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.GitLab;

public partial class GitLab
{
    private static HttpClient CreateHttpClient(string host, string accessToken, TimeSpan? timeout = null)
    {
        return new HttpClient
               {
                   Timeout = timeout ?? TimeSpan.FromSeconds(100),
                   BaseAddress = new Uri(host),
                   DefaultRequestHeaders =
                   {
                       UserAgent = { new ProductInfoHeaderValue("GRUKE", typeof(GitLab).Assembly.GetVersionText()) },
                       Authorization = AuthenticationHeaderValue.Parse($"Bearer {accessToken}")
                   }
               };
    }

    /// <summary>
    ///     Gets the NuGet package source for the current GitLab project.
    ///     <br/><br/>
    ///     This method is expected to be executed in a CI environment, as it depends on:
    ///         <list type="number">
    ///             <item>
    ///                 <see cref="ProjectId"/>
    ///             </item>
    ///         </list>
    /// </summary>
    /// <returns></returns>
    public string GetNuGetSourceUrlForCurrentProject(string apiBaseUrl = "https://gitlab.com/api/v4/")
    {
        return GetNuGetSourceUrlForSpecificProject(ProjectId, apiBaseUrl);
    }

    /// <summary>
    ///     Gets the NuGet package source for a specific GitLab project.
    /// </summary>
    /// <returns></returns>
    public static string GetNuGetSourceUrlForSpecificProject(long projectId, string apiBaseUrl = "https://gitlab.com/api/v4/")
    {
        apiBaseUrl = apiBaseUrl.EndsWith('/') ? apiBaseUrl : apiBaseUrl + '/';

        return $"{apiBaseUrl}projects/{projectId}/packages/nuget/index.json";
    }

    /// <summary>
    ///     Uploads a generic package to a specific project's Package Registry.
    /// </summary>
    /// <param name="upload">The path of the file to upload.</param>
    /// <param name="projectPath">The 'namespace/subpath' of the project.</param>
    /// <param name="accessToken">The GitLab API access token with write access to the target project's Package Registry.</param>
    /// <param name="packageName">The name of the generic package.</param>
    /// <param name="packageVersion">The version currently being uploaded to.</param>
    /// <param name="apiUrl">The API base URL. Defaults to GitLab Cloud's API.</param>
    /// <returns>null if HTTP 2xx is returned; an error string otherwise.</returns>
    [ItemCanBeNull]
    public static async Task<string> UploadGenericPackageToSpecificProjectAsync(
        AbsolutePath upload,
        string projectPath,
        string accessToken,
        string packageName,
        string packageVersion,
        string apiUrl = "https://gitlab.com/api/v4/"
    )
    {
        using var http = CreateHttpClient(
            apiUrl.EndsWith('/')
                ? apiUrl
                : apiUrl + '/',
            accessToken,
            TimeSpan.FromMinutes(5)
        );

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

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return "Invalid authorization.";

            if (response.StatusCode == HttpStatusCode.NotFound)
                return "Invalid authorization.";

            if (response.StatusCode == HttpStatusCode.Forbidden)
                return $"Project {projectPath.SingleQuoteIfNeeded()} has the package registry disabled.";

            return response.IsSuccessStatusCode ? null : response.ReasonPhrase;
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
    ///     Uploads a generic package to the current project's Package Registry.
    ///     <br/> <br/>
    ///     This method is expected to be executed in a CI environment, as it depends on:
    ///         <list type="number">
    ///             <item>
    ///                 <see cref="ApiV4Url"/>
    ///             </item>
    ///             <item>
    ///                 <see cref="JobToken"/>
    ///             </item>
    ///             <item>
    ///                 <see cref="ProjectId"/>
    ///             </item>
    ///             <item>
    ///                 <see cref="ProjectPath"/>
    ///             </item>
    ///         </list>
    /// </summary>
    /// <param name="packageName">The name of the generic package.</param>
    /// <param name="packageVersion">The version currently being uploaded to.</param>
    /// <param name="upload">The path of the file to upload.</param>
    /// <returns>true if HTTP 2xx is returned; false otherwise.</returns>
    [ItemCanBeNull]
    public Task<string> UploadGenericPackageToCurrentProjectAsync(
        AbsolutePath upload,
        string packageName,
        string packageVersion)
    {
        return UploadGenericPackageToSpecificProjectAsync(upload, ProjectPath, JobToken, packageName, packageVersion, ApiV4Url);
    }
}
