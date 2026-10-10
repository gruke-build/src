// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Nuke.Common.Execution;

[Serializable]
internal class TargetExecutionException : Exception
{
    public TargetExecutionException(string targetName, Exception inner)
        : base($"Target '{targetName}' has thrown an exception.", inner)
    {
    }

    protected TargetExecutionException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}
