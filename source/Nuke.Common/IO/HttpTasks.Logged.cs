// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.Utilities.Net;
using Serilog;

namespace Nuke.Common.IO;

public static partial class HttpTasks
{
    [Pure]
    public static async Task<string> HttpDownloadStringLoggedAsync(
        string uri,
        Configure<HttpClientProxy> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        var httpClient = CreateHttpClientProxy(clientConfigurator, headerConfigurator);
        return await (await httpClient.GetAsync(uri))!.Content.ReadAsStringAsync();
    }

    [Pure]
    public static string HttpDownloadStringLogged(
        string uri,
        Configure<HttpClientProxy> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        return HttpDownloadStringLoggedAsync(uri, clientConfigurator, headerConfigurator).Result;
    }

    public static async Task HttpDownloadFileLoggedAsync(
        string uri,
        AbsolutePath path,
        FileMode mode = FileMode.Create,
        Configure<HttpClientProxy> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        var httpClient = CreateHttpClientProxy(clientConfigurator, headerConfigurator);
        var response = await httpClient.GetAsync(uri);
        Assert.True(response.IsSuccessStatusCode, $"{response.ReasonPhrase}: {uri}");

        path.Parent.CreateDirectory();
        await using var fileStream = File.Open(path, mode);
        await response.Content.CopyToAsync(fileStream);
    }

    public static void HttpDownloadFileLogged(
        string uri,
        string path,
        FileMode mode = FileMode.Create,
        Configure<HttpClientProxy> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        HttpDownloadFileLoggedAsync(uri, path, mode, clientConfigurator, headerConfigurator).Wait();
    }

    private static IHttpClientProxy CreateHttpClientProxy(
        Configure<HttpClientProxy> clientConfigurator = null,
        Action<HttpRequestHeaders> headerConfigurator = null)
    {
        var httpClient = new HttpClientProxy(new HttpClient { Timeout = DefaultTimeout },
            (fmt, args, lines, _) =>
            {
#pragma warning disable CA2254
                Log.Information(fmt, args);
                lines.ForEach(line => Log.Error(line));
#pragma warning restore CA2254
            });
        clientConfigurator?.Invoke(httpClient);
        headerConfigurator?.Invoke(httpClient.DefaultRequestHeaders);
        return httpClient;
    }
}
