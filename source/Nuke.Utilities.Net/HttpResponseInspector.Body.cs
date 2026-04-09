// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nuke.Common.IO;

namespace Nuke.Common.Utilities.Net;

// TODO: reduce with overloads
public partial class HttpResponseInspector
{
    /// <summary>
    /// Reads the HTTP response body as JSON.
    /// </summary>
    public async Task<T> GetBodyAsJson<T>()
    {
        return JsonConvert.DeserializeObject<T>(await GetBodyAsync());
    }

    /// <summary>
    /// Reads the HTTP response body as JSON.
    /// </summary>
    public async Task<JObject> GetBodyAsJson()
    {
        return await GetBodyAsJson<JObject>();
    }

    /// <summary>
    /// Reads the HTTP response body as JSON.
    /// </summary>
    public async Task<T> GetBodyAsJson<T>(JsonSerializerSettings settings)
    {
        return JsonConvert.DeserializeObject<T>(await GetBodyAsync(), settings);
    }

    /// <summary>
    /// Reads the HTTP response body as JSON.
    /// </summary>
    public async Task<JObject> GetBodyAsJson(JsonSerializerSettings settings)
    {
        return await GetBodyAsJson<JObject>(settings);
    }

    public async Task WriteToFile(AbsolutePath path, FileMode mode = FileMode.Create)
    {
        using var fileStream = File.Open(path, mode);
        await Response.Content.CopyToAsync(fileStream);
    }
}
