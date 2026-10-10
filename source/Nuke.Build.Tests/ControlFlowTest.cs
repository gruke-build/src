// Copyright 2023 Maintainers of NUKE.
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using FluentAssertions;
using Xunit;

// ReSharper disable ArgumentsStyleLiteral

namespace Nuke.Common.Tests;

public class ControlFlowTest
{
    [Fact]
    public void Test()
    {
        var executions = 0;

        void OnSecondExecution()
        {
            executions++;
            if (executions != 2)
                throw new Exception(executions.ToString());
        }

        ControlFlow.ExecuteWithRetry(OnSecondExecution);
        executions.Should().Be(2);
    }
}
