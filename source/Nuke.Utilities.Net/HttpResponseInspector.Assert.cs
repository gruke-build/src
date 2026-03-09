// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Nuke.Common.Utilities.Net;

public partial class HttpResponseInspector
{
    /// <summary>
    /// Asserts the status code of an HTTP response.
    /// </summary>
    public HttpResponseInspector AssertStatusCode(
        HttpStatusCode status,
        Func<HttpResponseMessage, string> errorSelector = null)
    {
        var response = Response;
        if (response.StatusCode != status)
        {
            throw new HttpResponseException(
                errorSelector == null
                    ? $"Expected status code {status} but found {response.StatusCode}"
                    : errorSelector.Invoke(response));
        }

        return this;
    }

    /// <summary>
    /// Asserts the status code of an HTTP response.
    /// </summary>
    public HttpResponseInspector AssertStatusCode(Func<HttpStatusCode, string> errorSelector)
    {
        var response = Response;
        if (errorSelector.Invoke(response.StatusCode) is { } error)
            throw new HttpResponseException(error);

        return this;
    }

    /// <summary>
    /// Asserts a successful status code for an HTTP response.
    /// </summary>
    public HttpResponseInspector AssertSuccessfulStatusCode()
    {
        Response.EnsureSuccessStatusCode();
        return this;
    }

    /// <summary>
    /// Asserts an HTTP response.
    /// </summary>
    public HttpResponseInspector AssertResponse(Func<HttpResponseMessage, string> errorSelector)
    {
        var response = Response;
        if (errorSelector.Invoke(response) is { } error)
            throw new HttpResponseException(error);

        return this;
    }
}

[Serializable]
public class HttpResponseException : Exception
{
    public HttpResponseException()
    {
    }

    public HttpResponseException(string message)
        : base(message)
    {
    }

    public HttpResponseException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected HttpResponseException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}
