// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.IO;
using System.Linq;
using Nuke.Common.CI.SpaceAutomation;

namespace Nuke.Common.Tests.CI;

#pragma warning disable CS0618 // Type or member is obsolete
public class TestSpaceAutomationAttribute : SpaceAutomationAttribute, ITestConfigurationGenerator
#pragma warning restore CS0618 // Type or member is obsolete
{
    public TestSpaceAutomationAttribute(string jobName, string image)
        : base(jobName, image)
    {
    }

    public StreamWriter Stream { get; set; }

    protected override StreamWriter CreateStream()
    {
        return Stream;
    }
}
