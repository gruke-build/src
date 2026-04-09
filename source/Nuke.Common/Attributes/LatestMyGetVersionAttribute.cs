// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Nuke.Common.IO;
using Nuke.Common.Utilities;
using Nuke.Common.ValueInjection;

namespace Nuke.Common.Tooling;

[PublicAPI]
public class LatestMyGetVersionAttribute : ValueInjectionAttributeBase
{
    private readonly string _feed;
    private readonly string _package;
    private readonly bool _logRequestDestination;

    public LatestMyGetVersionAttribute(string feed, string package, bool useLogging = true)
    {
        _feed = feed;
        _package = package;
        _logRequestDestination = useLogging;
    }

    private string Url => $"https://www.myget.org/RSS/{_feed}";

    public override object GetValue(MemberInfo member, object instance)
    {
        var content = _logRequestDestination
            ? HttpTasks.HttpDownloadStringLogged(Url)
            : HttpTasks.HttpDownloadString(Url);

        return XmlTasks.XmlPeekFromString(content, ".//title")
            // TODO: regex?
            .First(x => x.Contains($"/{_package} "))
            .Split('(').Last()
            .Split(')').First()
            .TrimStart("version ");
    }
}
