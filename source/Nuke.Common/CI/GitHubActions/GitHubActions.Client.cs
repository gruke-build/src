// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.GitHubActions;

public partial class GitHubActions
{
    /// <summary>
    /// Will not do anything without a <c>GITHUB_TOKEN</c> environment variable set.
    /// </summary>
    public async Task CreateComment(int issue, string text)
    {
        if (_httpClient.Value is null)
            return;

        await _httpClient.Value
            .CreateRequest(HttpMethod.Post, $"repos/{Repository}/issues/{issue}/comments")
            .WithJsonContent(new { body = text })
            .GetResponseAsync();
    }

    private JObject GetJobDetails(long runId)
    {
        // ReSharper disable once UseNullPropagation
        if (_httpClient.Value is null)
            return null;

        var response = _httpClient.Value
            .CreateRequest(HttpMethod.Get, $"repos/{Repository}/actions/runs/{runId}/jobs")
            .GetResponse()
            .AssertSuccessfulStatusCode();

        return response.GetBodyAsJson().GetAwaiter().GetResult()
            .GetChildren("jobs")
            .Single(x => x.GetPropertyStringValue("name") == Job);
    }

    private long GetJobId()
    {
        return GetJobDetails(RunId)?.GetPropertyValue<long>("id") ?? long.MinValue;
    }
}
