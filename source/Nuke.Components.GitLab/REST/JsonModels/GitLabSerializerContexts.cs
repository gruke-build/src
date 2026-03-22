// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nuke.Components.GitLab;

[JsonSerializable(typeof(IEnumerable<GitLabReleaseJsonResponse>))]
[JsonSerializable(typeof(IEnumerable<GetProjectPackagesItem>))]
[JsonSerializable(typeof(IEnumerable<GetPackageFilesItem>))]
[JsonSerializable(typeof(GitLabReleaseJsonResponse[]))]
[JsonSerializable(typeof(GetProjectPackagesItem[]))]
[JsonSerializable(typeof(GetPackageFilesItem[]))]
public partial class GitLabSerializerContexts : JsonSerializerContext;
