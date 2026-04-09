// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Net;
using System.Text.Json.Serialization;
using NGitLab.Models;
using Nuke.Common.Utilities.Net;

namespace Nuke.Components.GitLab;

public class GetProjectPackagesItem
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("version")] public string Version { get; set; } = null!;

    [JsonPropertyName("package_type")] public string PackageType { get; set; } = null!;

    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    public PaginatedEndpoint<GetPackageFilesItem> GetPackageFiles(IHttpClientProxy http, string projectPath)
    {
        return PaginatedEndpoint<GetPackageFilesItem>.Builder(http)
            .WithBaseUrl($"projects/{WebUtility.UrlEncode(projectPath)}/packages/{Id}/package_files")
            .WithJsonContentParser(GitLabSerializerContexts.Default.IEnumerableGetPackageFilesItem)
            .WithPerPageCount(100)
            .Build();
    }
}
