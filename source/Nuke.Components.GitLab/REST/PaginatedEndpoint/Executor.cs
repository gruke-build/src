// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Nuke.Common.Utilities.Collections;
using System.Threading.Tasks;

namespace Nuke.Components.GitLab;

#nullable enable

public partial class PaginatedEndpoint<T>
{
    public async Task<T?> FindOneAsync(Func<T, bool> predicate,
        Action<HttpStatusCode>? onNonSuccess = null)
    {
        var currentPage = 1;
        var response = await _http.GetAsync(GetUrl(currentPage));

        if (!(response?.IsSuccessStatusCode ?? false))
        {
            onNonSuccess?.Invoke(response?.StatusCode ?? (HttpStatusCode)999);
            return default;
        }

        IEnumerable<T> returned = await _parsePage(response.Content);

        if (returned.TryGetFirst(predicate, out var matched))
            return matched;

        var totalPages = response.Headers.GetValues("x-total-pages").FirstOrDefault();
        if (totalPages != null && int.TryParse(totalPages, out var pageCount) && pageCount > 1)
        {
            currentPage++;
            do
            {
                response = await _http.GetAsync(GetUrl(currentPage));

                if (!(response?.IsSuccessStatusCode ?? false))
                {
                    onNonSuccess?.Invoke(response?.StatusCode ?? (HttpStatusCode)999);
                    return default;
                }

                returned = await _parsePage(response.Content);

                if (returned.TryGetFirst(predicate, out matched))
                    return matched;

                currentPage++;
            } while (currentPage <= pageCount);
        }

        return default;
    }

    public async Task<T?> FindOneAsync(Action<HttpStatusCode>? onNonSuccess = null)
    {
        var currentPage = 1;
        var response = await _http.GetAsync(GetUrl(currentPage));

        if (!(response?.IsSuccessStatusCode ?? false))
        {
            onNonSuccess?.Invoke(response?.StatusCode ?? (HttpStatusCode)999);
            return default;
        }

        var returned = (await _parsePage(response.Content)).ToArray();
        if (returned.Length > 0)
            return returned[0];

        var totalPages = response.Headers.GetValues("x-total-pages").FirstOrDefault();
        if (totalPages != null && int.TryParse(totalPages, out var pageCount) && pageCount > 1)
        {
            currentPage++;
            do
            {
                response = await _http.GetAsync(GetUrl(currentPage));

                if (!(response?.IsSuccessStatusCode ?? false))
                {
                    onNonSuccess?.Invoke(response?.StatusCode ?? (HttpStatusCode)999);
                    return default;
                }

                returned = (await _parsePage(response.Content)).ToArray();
                if (returned.Length > 0)
                    return returned[0];

                currentPage++;
            } while (currentPage <= pageCount);
        }

        return default;
    }

    public async Task<IEnumerable<T>?> GetAllAsync(Func<T, bool> predicate,
        Action<HttpStatusCode>? onNonSuccess = null)
    {
        var currentPage = 1;
        var response = await _http.GetAsync(GetUrl(currentPage));

        if (!(response?.IsSuccessStatusCode ?? false))
        {
            onNonSuccess?.Invoke(response?.StatusCode ?? (HttpStatusCode)999);
            return null;
        }

        var accumulated = await _parsePage(response.Content);

        var totalPages = response.Headers.GetValues("x-total-pages").FirstOrDefault();
        if (totalPages != null && int.TryParse(totalPages, out var pageCount) && pageCount > 1)
        {
            currentPage++;
            do
            {
                response = await _http.GetAsync(GetUrl(currentPage));

                if (!(response?.IsSuccessStatusCode ?? false))
                {
                    onNonSuccess?.Invoke(response?.StatusCode ?? (HttpStatusCode)999);
                    return default;
                }

                accumulated = accumulated.Concat(await _parsePage(response.Content));

                currentPage++;
            } while (currentPage <= pageCount);
        }

        return accumulated.Where(predicate);
    }

    public async Task<IEnumerable<T>?> GetAllAsync(
        Action<HttpStatusCode>? onNonSuccess = null)
    {
        var currentPage = 1;
        var response = await _http.GetAsync(GetUrl(currentPage));

        if (!(response?.IsSuccessStatusCode ?? false))
        {
            onNonSuccess?.Invoke(response?.StatusCode ?? (HttpStatusCode)999);
            return default;
        }

        IEnumerable<T> accumulated = await _parsePage(response.Content);

        var totalPages = response.Headers.GetValues("x-total-pages").FirstOrDefault();
        if (totalPages != null && int.TryParse(totalPages, out var pageCount) && pageCount > 1)
        {
            currentPage++;
            do
            {
                response = await _http.GetAsync(GetUrl(currentPage));

                if (!(response?.IsSuccessStatusCode ?? false))
                {
                    onNonSuccess?.Invoke(response?.StatusCode ?? (HttpStatusCode)999);
                    return default;
                }

                accumulated = accumulated.Concat(await _parsePage(response.Content));

                currentPage++;
            } while (currentPage <= pageCount);
        }

        return accumulated;
    }
}
