// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Nuke.Common.Utilities.Net;

public partial class HttpClientProxy
{
    public partial class RequestBuilder
    {
        /// <summary>
        /// Executes the HTTP request and returns the response.
        /// </summary>
        public async Task<HttpResponseInspector> GetResponseAsync(
            CancellationToken cancellationToken = default)
        {
            var response = await Client.SendAsync(Request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            return new HttpResponseInspector(response);
        }

        /// <summary>
        /// Executes the HTTP request and returns the response.
        /// </summary>
        public HttpResponseInspector GetResponse()
        {
            return GetResponseAsync().GetAwaiter().GetResult();
        }
    }
}

public partial class HttpResponseInspector
{
    private string _body;

    public HttpResponseInspector(HttpResponseMessage response)
    {
        Response = response;
    }

    public HttpResponseMessage Response { get; }

    public async Task<string> GetBodyAsync()
    {
        _body ??= await Response.Content.ReadAsStringAsync();
        return _body;
    }
}
