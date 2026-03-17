// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.Utilities.Net;

#nullable enable

/// <summary>
/// A simple wrapper around <see cref="HttpClient"/> which calls the specified callback with (non-private)
/// information about all requests issued via this class.
/// <br/>
/// This class is non-owning of the passed <see cref="HttpClient"/>. The caller is responsible for disposal, if needed.
/// </summary>
[PublicAPI]
public partial class HttpClientProxy : IHttpClientProxy
{
    public delegate void LogCallback(string fmt, object[] fmtArgs, string[] extraLines, string caller);

    private readonly HttpClient _http;
    private readonly LogCallback? _callback;

    public static HttpClientProxy CreateStdOut(HttpClient backingClient)
    {
        return new HttpClientProxy(backingClient,
            (format, args, extraLines, caller) =>
            {
                Console.WriteLine($"{caller}: {(args.Length == 0 ? format : string.Format(format, args))}");
                extraLines.ForEach(line => Console.WriteLine(line));
            }
        );
    }

    /// <summary>
    ///     Creates a new <see cref="HttpClientProxy"/> around the provided <see cref="HttpClient"/>, directing all log output to a no-operation callback.
    ///     Use this to call into <see cref="HttpClientProxy"/> APIs when you don't care about the log output.
    /// </summary>
    /// <param name="backingClient">The <see cref="HttpClient"/> to wrap operations of.</param>
    public static HttpClientProxy CreateBasicWrapper(HttpClient backingClient)
    {
        return new HttpClientProxy(backingClient);
    }

    public HttpClientProxy(HttpClient httpClient, LogCallback? logCallback = null)
    {
        _http = httpClient;
        _callback = logCallback;
    }

    public Version DefaultRequestVersion
    {
        get => _http.DefaultRequestVersion;
        set => _http.DefaultRequestVersion = value;
    }

    public HttpVersionPolicy DefaultVersionPolicy
    {
        get => _http.DefaultVersionPolicy;
        set => _http.DefaultVersionPolicy = value;
    }

    public Uri? BaseAddress
    {
        get => _http.BaseAddress;
        set => _http.BaseAddress = value;
    }

    public TimeSpan Timeout
    {
        get => _http.Timeout;
        set => _http.Timeout = value;
    }

    public long MaxResponseContentBufferSize
    {
        get => _http.MaxResponseContentBufferSize;
        set => _http.MaxResponseContentBufferSize = value;
    }

    public HttpRequestHeaders DefaultRequestHeaders
    {
        get => _http.DefaultRequestHeaders;
    }

    [SuppressMessage("ReSharper", "RedundantAssignment",
        Justification =
            "ReSharper cannot comprehend the idea of checking all combinations of 2 objects potentially being null.")]
    public async Task<HttpResponseMessage?> SendAsync(HttpRequestMessage request, HttpCompletionOption? option = null,
        CancellationToken? token = null)
    {
        HttpResponseMessage? response = null;

        var sw = Stopwatch.StartNew();

        try
        {
            if (option is null && token is not null)
                response = await _http.SendAsync(request, token.Value);
            if (option is not null && token is null)
                response = await _http.SendAsync(request, option.Value);
            if (option is not null && token is not null)
                response = await _http.SendAsync(request, option.Value, token.Value);
            else
                response = await _http.SendAsync(request);

            sw.Stop();

            Log("{0} {1} -> {2} in {3}ms", GetLogArgs(request, sw, response));
        }
        catch (Exception e)
        {
            sw.Stop();
            Log("{0} {1} -> {2} in {3}ms", GetLogArgs(request, sw), 
                new[] { e.Message }
                    .Concat(e.StackTrace?.Split('\n') ?? [])
                    .ToArray());
        }

        return response;
    }

    private void Log(string messageFormat, object[]? formatArgs = null, string[]? extraLines = null, [CallerMemberName] string caller = null!)
    {
        _callback?.Invoke(messageFormat, formatArgs ?? [], extraLines ?? [], caller);
    }

    private object[] GetLogArgs(HttpRequestMessage request, Stopwatch sw, HttpResponseMessage? response = null)
    {
        var result = new object[4];
        result[0] = request.Method.Method;
        result[1] = request.RequestUri!.ToString();
        result[2] = ((int?)response?.StatusCode)?.ToString() ?? "ERR";
        result[3] = sw.Elapsed.TotalMilliseconds;
        return result;
    }
}
