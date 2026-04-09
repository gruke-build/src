// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using FluentAssertions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Xunit;

namespace Nuke.Common.Tests;

public class SolutionModelTest
{
    private static AbsolutePath RootDirectory => Constants.TryGetRootDirectoryFrom(EnvironmentInfo.WorkingDirectory).NotNull();

    private static AbsolutePath SolutionFile => RootDirectory / "Gruke.slnx";

    [Fact]
    public void SolutionTest()
    {
        var solution = SolutionFile.ReadSolution();

        solution.SolutionFolders.Select(x => x.Name).Should().BeEquivalentTo("misc");

        var buildProject = solution.AllProjects.SingleOrDefault(x => x.Name == "_build");
        buildProject.Should().NotBeNull();

        // solution.SaveAs(solution.Path + ".bak");
    }

    [Fact]
    public void SolutionGetProjectsTest()
    {
        var solution = SolutionFile.ReadSolution();

        solution.GetAllProjects("*.Tests").Should().HaveCountGreaterOrEqualTo(2);
    }
}
