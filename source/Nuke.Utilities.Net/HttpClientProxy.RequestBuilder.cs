// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace Nuke.Common.Utilities.Net;

public partial class HttpClientProxy
{
    [PublicAPI]
    public partial class RequestBuilder
    {
        public RequestBuilder(HttpClientProxy client, HttpRequestMessage request)
        {
            Client = client;
            Request = request;
        }

        public HttpClientProxy Client { get; }
        public HttpRequestMessage Request { get; }
    }

    /// <summary>
    /// Creates an HTTP request.
    /// </summary>
    public RequestBuilder CreateRequest(HttpMethod method, string relativeUri)
    {
        return new RequestBuilder(this, IHttpClientProxy.CreateRequestMessage(method, new Uri(relativeUri)));
    }

    /// <summary>
    /// Creates an HTTP request.
    /// </summary>
    public RequestBuilder CreateRequest(HttpMethod method, string baseAddress, string relativeUri)
    {
        return new RequestBuilder(this,
            IHttpClientProxy.CreateRequestMessage(method,
                new Uri(new Uri(baseAddress), relativeUri)
            )
        );
    }
}
