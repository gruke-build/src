// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

// ReSharper disable ArrangeMethodOrOperatorBody

#nullable enable

namespace Nuke.Common.Utilities.Net;

[PublicAPI]
public interface IHttpClientProxy : IDisposable
{
    public HttpRequestHeaders DefaultRequestHeaders { get; }
    public Version DefaultRequestVersion { get; set; }
    public HttpVersionPolicy DefaultVersionPolicy { get; set; }
    public Uri? BaseAddress { get; set; }

    public TimeSpan Timeout { get; set; }
    public long MaxResponseContentBufferSize { get; set; }

    public Task<HttpResponseMessage?> SendAsync(HttpRequestMessage request, HttpCompletionOption? option = null,
        CancellationToken? token = null);

    #region Convenience overloads for SendAsync

    public Task<HttpResponseMessage?> GetAsync(
        [StringSyntax(StringSyntaxAttribute.Uri)]
        string requestUri,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => GetAsync(CreateUri(requestUri)!, option, token);

    public Task<HttpResponseMessage?> PostAsync(
        [StringSyntax(StringSyntaxAttribute.Uri)]
        string requestUri,
        HttpContent? content = null,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => PostAsync(CreateUri(requestUri)!, content, option, token);

    public Task<HttpResponseMessage?> PutAsync(
        [StringSyntax(StringSyntaxAttribute.Uri)]
        string requestUri,
        HttpContent? content = null,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => PutAsync(CreateUri(requestUri)!, content, option, token);

    public Task<HttpResponseMessage?> PatchAsync(
        [StringSyntax(StringSyntaxAttribute.Uri)]
        string requestUri,
        HttpContent? content = null,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => PatchAsync(CreateUri(requestUri)!, content, option, token);

    public Task<HttpResponseMessage?> DeleteAsync(
        [StringSyntax(StringSyntaxAttribute.Uri)]
        string requestUri,
        HttpContent? content = null,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => DeleteAsync(CreateUri(requestUri)!, content, option, token);

    #region Uri overloads

    public Task<HttpResponseMessage?> GetAsync(
        Uri requestUri,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => SendAsync(CreateRequestMessage(HttpMethod.Get, requestUri), option, token);

    public Task<HttpResponseMessage?> PostAsync(
        Uri requestUri,
        HttpContent? content = null,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => SendAsync(CreateRequestMessageWithContent(HttpMethod.Post, requestUri, content), option, token);

    public Task<HttpResponseMessage?> PutAsync(
        Uri requestUri,
        HttpContent? content = null,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => SendAsync(CreateRequestMessageWithContent(HttpMethod.Put, requestUri, content), option, token);

    public Task<HttpResponseMessage?> PatchAsync(
        Uri requestUri,
        HttpContent? content = null,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => SendAsync(CreateRequestMessageWithContent(HttpMethod.Patch, requestUri, content), option, token);

    public Task<HttpResponseMessage?> DeleteAsync(
        Uri requestUri,
        HttpContent? content = null,
        HttpCompletionOption? option = null, CancellationToken? token = null
    ) => SendAsync(CreateRequestMessageWithContent(HttpMethod.Delete, requestUri, content), option, token);

    #endregion

    #endregion

    #region Overload Helpers

    protected static HttpRequestMessage CreateRequestMessage(HttpMethod method, Uri? uri)
        => new(method, uri)
            { Version = HttpVersion.Version11, VersionPolicy = HttpVersionPolicy.RequestVersionOrLower };

    protected static HttpRequestMessage CreateRequestMessageWithContent(HttpMethod method, Uri? uri,
        HttpContent? requestContent)
    {
        var req = CreateRequestMessage(method, uri);
        req.Content = requestContent;
        return req;
    }

    private static Uri? CreateUri(string? uri) =>
        string.IsNullOrEmpty(uri) ? null : new Uri(uri, UriKind.RelativeOrAbsolute);

    #endregion
}
