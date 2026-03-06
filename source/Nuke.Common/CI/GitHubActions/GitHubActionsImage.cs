// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Tooling;

namespace Nuke.Common.CI.GitHubActions;

/// <summary>
/// See <a href="https://docs.github.com/en/actions/using-github-hosted-runners/about-github-hosted-runners">Virtual environments for GitHub Actions</a>
/// </summary>
[PublicAPI]
public enum GitHubActionsImage
{
    [EnumValue("windows-11-arm")] Windows11Arm,
    [EnumValue("ubuntu-24.04-arm")] Ubuntu2404Arm,
    [EnumValue("ubuntu-22.04-arm")] Ubuntu2204Arm,
    [EnumValue("macos-26")] MacOs26TahoeArm,
    [EnumValue("macos-26-xlarge")] MacOs26TahoeLargeArm,
    [EnumValue("macos-15")] MacOs15SequoiaArm,
    [EnumValue("macos-15-xlarge")] MacOs15SequoiaLargeArm,
    [EnumValue("macos-14")] MacOs14SonomaArm,
    [EnumValue("macos-14-xlarge")] MacOs14SonomaLargeArm,

    [EnumValue("windows-2025")] WindowsServer2025,
    [EnumValue("windows-2025-vs2026")] WindowsServer2025WithVs2026,
    [EnumValue("windows-2022")] WindowsServer2022,
    [EnumValue("ubuntu-24.04")] Ubuntu2404,
    [EnumValue("ubuntu-22.04")] Ubuntu2204,
    [EnumValue("ubuntu-slim")] UbuntuSlim,
    [EnumValue("macos-26-intel")] MacOs26TahoeIntel,
    [EnumValue("macos-26-large")] MacOs26TahoeLargeIntel,
    [EnumValue("macos-15-intel")] MacOs15SequoiaIntel,
    [EnumValue("macos-15-large")] MacOs15SequoiaLargeIntel,
    [EnumValue("macos-14-large")] MacOs14SonomaLargeIntel,
    [EnumValue("windows-latest")] WindowsLatest,
    [EnumValue("ubuntu-latest")] UbuntuLatest,
    [EnumValue("macos-latest")] MacOsLatest,
    [EnumValue("self-hosted")] SelfHosted
}
