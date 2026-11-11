// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuke.Common.IO;
using Nuke.Common.Utilities;
using Serilog;

namespace Nuke.Common.CI.GitHubActions;

public partial class GitHubActions
{
    /// <summary>
    ///     Asynchronously obtain a short-lived NuGet API key by exchanging the GitHub OIDC token with any NuGet-compatible token service
    /// </summary>
    /// <param name="username">Your NuGet account username.</param>
    /// <param name="tokenServiceUrl">URL to your NuGet server's token endpoint</param>
    /// <param name="audience">OIDC audience</param>
    /// <param name="logRequestUrls">Automatically log URLs requested (no auth information is shown)</param>
    /// <returns>The short-lived API key returned by the NuGet token service.</returns>
    /// <remarks>This is a C# reimplementation of the <a href="https://github.com/NuGet/login">NuGet/login</a> GitHub Actions reusable workflow.</remarks>
    public async Task<string> GetNuGetApiKeyWithOpenIdAsync(
        [DisallowNull] string username,
        [DisallowNull] string tokenServiceUrl = "https://www.nuget.org/api/v2/token",
        [DisallowNull] string audience = "https://www.nuget.org",
        bool logRequestUrls = true)
    {
        var oidcRequestToken = OpenIdRequestToken
            .NotNullOrEmpty("ACTIONS_ID_TOKEN_REQUEST_TOKEN is missing. Ensure your workflow has 'id-token: write' in the 'permissions' block.");
        var oidcRequestUrl = OpenIdRequestUrl
            .NotNullOrEmpty("ACTIONS_ID_TOKEN_REQUEST_URL is missing. Ensure your workflow has 'id-token: write' in the 'permissions' block.");

        SetSecret(oidcRequestToken);

        var tokenUrl = $"{oidcRequestUrl}&audience={WebUtility.UrlEncode(audience)}";

        var tokenResponse = await (logRequestUrls
                ? HttpTasks.HttpGetLoggedAsync(tokenUrl, headerConfigurator: headers => headers.Add("Authorization", $"Bearer {oidcRequestToken}"))
                : HttpTasks.HttpGetAsync(tokenUrl, headerConfigurator: headers => headers.Add("Authorization", $"Bearer {oidcRequestToken}"))
            );

        if (!tokenResponse.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Failed to retrieve OIDC token from GitHub (HTTP {tokenResponse.StatusCode}). "
                                                + $"Verify that the audience is correct and that the token service URL is reachable.");
        }

        var responseBody = JsonConvert.DeserializeObject<JObject>(await tokenResponse.Content.ReadAsStringAsync());

        var oidcToken = responseBody.GetValue("value")?.Value<string>();
        if (string.IsNullOrEmpty(oidcToken))
        {
            throw new InvalidOperationException("Failed to read OIDC token from GitHub response.");
        }

        SetSecret(oidcToken);

        var body = JsonConvert.SerializeObject(new { username, tokenType = "ApiKey" });

        Action<HttpRequestHeaders> headers = headers =>
        {
            headers.Add("Authorization", $"Bearer {oidcToken}");
            headers.Add("User-Agent", $"GRUKE/{typeof(NukeBuild).Assembly.GetVersionText()}");
        };

        var nugetResponse = await (logRequestUrls
                ? HttpTasks.HttpPostLoggedAsync(tokenServiceUrl, new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json), headerConfigurator: headers)
                : HttpTasks.HttpPostAsync(tokenServiceUrl, new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json), headerConfigurator: headers)
            );

        var nugetResponseBody = await nugetResponse.Content.ReadAsStringAsync();

        if (nugetResponse.StatusCode != HttpStatusCode.OK)
        {
            throw new InvalidOperationException($"Token exchange failed (HTTP {nugetResponse.StatusCode}) at {tokenServiceUrl}. "
                                                + $"Make sure you are passing the username of the policy creator, not the policy owner: {nugetResponseBody}");
        }

        var parsedNuGetResponseBody = JsonConvert.DeserializeObject<JObject>(nugetResponseBody);

        var apiKey = parsedNuGetResponseBody.GetValue("apiKey")?.Value<string>();

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("Response did not contain 'apiKey'.");
        }

        SetSecret(apiKey);

        Log.Information("Successfully exchanged OIDC token for NuGet API key.");

        return apiKey;
    }

    /// <summary>
    ///     Synchronously obtain a short-lived NuGet API key by exchanging the GitHub OIDC token with any NuGet-compatible token service.
    /// </summary>
    /// <param name="username">Your NuGet account username.</param>
    /// <param name="tokenServiceUrl">URL to your NuGet server's token endpoint</param>
    /// <param name="audience">OIDC audience</param>
    /// <param name="logRequestUrls">Automatically log URLs requested (no auth information is shown)</param>
    /// <returns>The short-lived API key returned by the NuGet token service.</returns>
    /// <remarks>This is a C# reimplementation of the <a href="https://github.com/NuGet/login">NuGet/login</a> GitHub Actions reusable workflow.</remarks>
    public string GetNuGetApiKeyWithOpenId(
        [DisallowNull] string username,
        [DisallowNull] string tokenServiceUrl = "https://www.nuget.org/api/v2/token",
        [DisallowNull] string audience = "https://www.nuget.org",
        bool logRequestUrls = true)
    {
        var oidcRequestToken = OpenIdRequestToken
            .NotNullOrEmpty("ACTIONS_ID_TOKEN_REQUEST_TOKEN is missing. Ensure your workflow has 'id-token: write' in the 'permissions' block.");
        var oidcRequestUrl = OpenIdRequestUrl
            .NotNullOrEmpty("ACTIONS_ID_TOKEN_REQUEST_URL is missing. Ensure your workflow has 'id-token: write' in the 'permissions' block.");

        SetSecret(oidcRequestToken);

        var tokenUrl = $"{oidcRequestUrl}&audience={WebUtility.UrlEncode(audience)}";

        var tokenResponse = logRequestUrls
                ? HttpTasks.HttpGetLogged(tokenUrl, headerConfigurator: headers => headers.Add("Authorization", $"Bearer {oidcRequestToken}"))
                : HttpTasks.HttpGet(tokenUrl, headerConfigurator: headers => headers.Add("Authorization", $"Bearer {oidcRequestToken}"));

        if (!tokenResponse.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Failed to retrieve OIDC token from GitHub (HTTP {tokenResponse.StatusCode}). "
                                                + $"Verify that the audience is correct and that the token service URL is reachable.");
        }

        var responseBody = JsonConvert.DeserializeObject<JObject>(tokenResponse.Content.ReadAsStringAsync().Result);

        var oidcToken = responseBody.GetValue("value")?.Value<string>();
        if (string.IsNullOrEmpty(oidcToken))
        {
            throw new InvalidOperationException("Failed to read OIDC token from GitHub response.");
        }

        SetSecret(oidcToken);

        var body = JsonConvert.SerializeObject(new { username, tokenType = "ApiKey" });

        Action<HttpRequestHeaders> headers = headers =>
        {
            headers.Add("Authorization", $"Bearer {oidcToken}");
            headers.Add("User-Agent", $"GRUKE/{typeof(NukeBuild).Assembly.GetVersionText()}");
        };

        var nugetResponse = logRequestUrls
                ? HttpTasks.HttpPostLogged(tokenServiceUrl, new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json), headerConfigurator: headers)
                : HttpTasks.HttpPost(tokenServiceUrl, new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json), headerConfigurator: headers);

        var nugetResponseBody = nugetResponse.Content.ReadAsStringAsync();

        if (nugetResponse.StatusCode != HttpStatusCode.OK)
        {
            throw new InvalidOperationException($"Token exchange failed (HTTP {nugetResponse.StatusCode}) at {tokenServiceUrl}. "
                                                + $"Make sure you are passing the username of the policy creator, not the policy owner: {nugetResponseBody.Result}");
        }

        var parsedNuGetResponseBody = JsonConvert.DeserializeObject<JObject>(nugetResponseBody.Result);

        var apiKey = parsedNuGetResponseBody.GetValue("apiKey")?.Value<string>();

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("Response did not contain 'apiKey'.");
        }

        SetSecret(apiKey);

        Log.Information("Successfully exchanged OIDC token for NuGet API key.");

        return apiKey;
    }
}
