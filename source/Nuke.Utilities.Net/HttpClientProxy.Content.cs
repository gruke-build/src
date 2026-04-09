// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Nuke.Common.Utilities.Net;

public partial class HttpClientProxy
{
    public partial class RequestBuilder
    {
        /// <summary>
        /// Sets the JSON-serialized object as content via <see cref="JsonConvert.SerializeObject(object?)"/>.
        /// </summary>
        public RequestBuilder WithJsonContent<T>(T obj)
        {
            var content = JsonConvert.SerializeObject(obj);
            return WithStringContent(content, "application/json");
        }

        /// <summary>
        /// Sets the JSON-serialized object as content via <see cref="JsonConvert.SerializeObject(object?)"/>.
        /// </summary>
        public RequestBuilder WithJsonContent<T>(T obj, JsonSerializerSettings settings)
        {
            var content = JsonConvert.SerializeObject(obj, settings);
            return WithStringContent(content, "application/json");
        }

        /// <summary>
        /// Sets the string as content.
        /// </summary>
        public RequestBuilder WithStringContent(string content, string mediaType)
        {
            Request.Content = new StringContent(content, Encoding.UTF8, mediaType);
            return this;
        }

        /// <summary>
        /// Sets the dictionary as content via <see cref="FormUrlEncodedContent"/>.
        /// </summary>
        public RequestBuilder WithFormUrlEncodedContent(IDictionary<string, string> dictionary)
        {
            Request.Content = new FormUrlEncodedContent(dictionary);
            return this;
        }

        /// <summary>
        /// Sets a <see cref="MultipartFormDataContent"/> as content.
        /// </summary>
        public RequestBuilder WithMultipartFormDataContent(
            Func<MultipartFormDataContent, MultipartFormDataContent> configurator)
        {
            Request.Content = configurator.Invoke(new MultipartFormDataContent());
            return this;
        }
    }
}

public static partial class HttpRequestExtensions
{
    /// <summary>
    /// Adds a string as content to a <see cref="MultipartFormDataContent"/>.
    /// </summary>
    public static MultipartFormDataContent AddStringContent(this MultipartFormDataContent data, string name, string content)
    {
        data.Add(new StringContent(content), name);
        return data;
    }

    /// <summary>
    /// Adds a stream as content to a <see cref="MultipartFormDataContent"/>.
    /// </summary>
    public static MultipartFormDataContent AddStreamContent(
        this MultipartFormDataContent data,
        string name,
        Stream content,
        string filename,
        string mediaType = null)
    {
        var streamContent = new StreamContent(content);
        if (mediaType != null)
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

        data.Add(streamContent, name, filename);
        return data;
    }
}
