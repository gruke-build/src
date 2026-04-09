// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Text.Json.Serialization;

namespace Nuke.Components.GitLab;

public class GitLabReleaseJsonResponse
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("created_at")] public DateTimeOffset CreatedAt { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = null!;
    [JsonPropertyName("tag_name")] public string TagName { get; set; } = null!;
    [JsonPropertyName("author")] public GitLabUserJsonResponse Author { get; set; } = null!;
    [JsonPropertyName("_links")] public WebLinks Links { get; set; } = null!;
    [JsonIgnore] public string Url => Links.Self;
    [JsonPropertyName("assets")] public GitLabReleaseAssetsJsonResponse Assets { get; set; } = null!;

    public class GitLabReleaseAssetsJsonResponse
    {
        [JsonPropertyName("links")] public AssetLink[] Links { get; set; } = null!;
    }

    public class AssetLink
    {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("name")] public string AssetName { get; set; } = null!;
        [JsonPropertyName("url")] public string Url { get; set; } = null!;
    }


    public class GitLabUserJsonResponse
    {
        [JsonPropertyName("id")] public long Id { get; set; }
        [JsonPropertyName("username")] public string Username { get; set; } = null!;
        [JsonPropertyName("name")] public string Name { get; set; } = null!;
        [JsonPropertyName("avatar_url")] public string AvatarUrl { get; set; } = null!;
    }

    public class WebLinks
    {
        [JsonPropertyName("self")] public string Self { get; set; } = null!;
    }
}
