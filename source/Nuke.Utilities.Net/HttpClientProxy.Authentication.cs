// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Nuke.Common.Utilities.Net;

public partial class HttpClientProxy
{
    public partial class RequestBuilder
    {
        /// <summary>
        /// Sets the bearer token for authentication.
        /// </summary>
        public RequestBuilder WithBearerAuthentication(string bearerToken)
        {
            return WithAuthentication("Bearer", bearerToken);
        }

        /// <summary>
        /// Sets the username and password for authentication.
        /// </summary>
        public RequestBuilder WithBasicAuthentication(string username, string password)
        {
            return WithAuthentication("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
        }

        /// <summary>
        /// Sets the scheme and parameter for authentication.
        /// </summary>
        public RequestBuilder WithAuthentication(string scheme, string parameter)
        {
            Request.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);
            return this;
        }
    }
}
