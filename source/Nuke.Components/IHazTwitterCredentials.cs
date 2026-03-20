// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common;

namespace Nuke.Components;

[PublicAPI]
[ParameterPrefix(Twitter)]
public interface IHazTwitterCredentials : INukeBuild
{
    public const string Twitter = nameof(Twitter);

    [Parameter("Twitter API Key")] [Secret] string ConsumerKey => TryGetValue(() => ConsumerKey);
    [Parameter("Twitter API Key Secret")] [Secret] string ConsumerSecret => TryGetValue(() => ConsumerSecret);
    [Parameter("Twitter API Access Token")] [Secret] string AccessToken => TryGetValue(() => AccessToken);
    [Parameter("Twitter API Access Token Secret")] [Secret] string AccessTokenSecret => TryGetValue(() => AccessTokenSecret);
}
