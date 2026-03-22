// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Nuke.Common.Utilities.Net;

namespace Nuke.Components.GitLab;

#nullable enable
public partial class PaginatedEndpoint<T> where T : class
{
    private PaginatedEndpoint(IHttpClientProxy client,
        string baseUrl,
        HttpContentParser parsePage,
        Dictionary<string, object> queryStringParams,
        int perPage = 100)
    {
        _http = client;
        _baseUrl = baseUrl;
        _parsePage = parsePage;
        _queryStringParams = queryStringParams;
        _queryStringParams["per_page"] = perPage;
    }

    private readonly IHttpClientProxy _http;
    private readonly string _baseUrl;
    private readonly HttpContentParser _parsePage;
    private readonly Dictionary<string, object> _queryStringParams;
    private string? _constructedUrl;

    private string GetUrl(int pageNumber)
    {
        if (_constructedUrl is null)
        {
            var sb = new StringBuilder(_baseUrl.TrimEnd('/'));
            foreach (var (index, (param, value)) in _queryStringParams.Index())
            {
                sb.Append(index is 0 ? "?" : "&");

                sb.Append(param).Append('=').Append(value);
            }

            _constructedUrl = sb.ToString();
        }

        return $"{_constructedUrl}&page={pageNumber}";
    }

    public delegate Task<IEnumerable<T>> HttpContentParser(HttpContent content);
}
