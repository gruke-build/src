// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.TeamCity.Configuration;

[PublicAPI]
public class TeamCityFinishBuildTrigger : TeamCityTrigger
{
    public TeamCityBuildType BuildType { get; set; }

    public override void Write(CustomFileWriter writer)
    {
        using (writer.WriteKotlinLambda("finishBuildTrigger"))
        {
            writer.WriteLine($"buildType = {$"${{{BuildType.Id}.id}}".DoubleQuote()}");
        }
    }
}
