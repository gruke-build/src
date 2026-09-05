// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using Newtonsoft.Json;
using System;

namespace Nuke.Common.Tools.Teams;

public partial class TeamsMessage
{
    [JsonProperty("@type")]
    internal string Type => "MessageCard";
    [JsonProperty("@context")]
    internal string Context => "http://schema.org/extensions";
}
