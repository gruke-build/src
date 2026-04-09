// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization.Metadata;
using Nuke.Common.Utilities.Net;

namespace Nuke.Components.GitLab;

public partial class PaginatedEndpoint<T>
{
    public static BuilderApi Builder(IHttpClientProxy httpClient) => new(httpClient);

    public static BuilderApi Builder(HttpClient httpClient) => new(new HttpClientProxy(httpClient));

    public class BuilderApi
    {
        public BuilderApi(IHttpClientProxy httpClient)
        {
            _http = httpClient;
        }

        private readonly IHttpClientProxy _http;

        public string BaseUrl { get; private set; } = null!;
        public HttpContentParser ContentParser { get; private set; } = null!;
        public int PerPage { get; private set; } = 100;

        public Dictionary<string, object> QueryStringParameters { get; private set; } = new();

        public BuilderApi WithBaseUrl(string url)
        {
            BaseUrl = url;
            return this;
        }

        public BuilderApi WithContentParser(HttpContentParser contentParser)
        {
            ContentParser = contentParser;
            return this;
        }

        public BuilderApi WithJsonContentParser(JsonTypeInfo<IEnumerable<T>> typeInfo)
        {
            ContentParser = content => content.ReadFromJsonAsync(typeInfo)!;
            return this;
        }

        public BuilderApi WithPerPageCount(int perPage)
        {
            PerPage = perPage;
            return this;
        }

        public BuilderApi WithQueryStringParameters(params (string, object)[] parameters)
        {
            QueryStringParameters = parameters.ToDictionary(x => x.Item1, x => x.Item2);
            return this;
        }

        public PaginatedEndpoint<T> Build()
        {
            return new PaginatedEndpoint<T>(_http, BaseUrl, ContentParser, QueryStringParameters, PerPage);
        }

        public static implicit operator PaginatedEndpoint<T>(BuilderApi builder)
        {
            return builder.Build();
        }
    }
}
