// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Text.Json.Serialization;

namespace Nuke.Components.GitLab;

public class GetPackageFilesItem
{
    [JsonPropertyName("id")] public long Id { get; set; }

    [JsonPropertyName("package_id")] public long PackageId { get; set; }

    [JsonPropertyName("file_name")] public string Name { get; set; } = null!;

    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("size")] public long Size { get; set; }
}
