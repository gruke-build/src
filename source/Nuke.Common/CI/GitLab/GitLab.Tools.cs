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
using Nuke.Common.Utilities.Collections;
using Nuke.Common.Utilities.Net;
using Serilog;

namespace Nuke.Common.CI.GitLab;

public partial class GitLab
{
    private static IHttpClientProxy CreateHttpClient(string host, string accessToken, TimeSpan? timeout = null)
    {
        return new HttpClientProxy(new HttpClient
                                   {
                                       Timeout = timeout ?? TimeSpan.FromSeconds(100),
                                       BaseAddress = new Uri(host),
                                       DefaultRequestHeaders =
                                       {
                                           UserAgent = { new ProductInfoHeaderValue("GRUKE", typeof(GitLab).Assembly.GetVersionText()) },
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

    public IHttpClientProxy CreateHttpClient(TimeSpan? timeout = null)
    {
        return CreateHttpClient(ApiV4Url.EnsureEnding('/'), JobToken, timeout);
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
}
