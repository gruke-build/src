// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using JetBrains.Annotations;

namespace Nuke.Common.CI;

public interface IBuildServer
{
    [CanBeNull]
    string Branch { get; }

    [CanBeNull]
    string Commit { get; }
}
