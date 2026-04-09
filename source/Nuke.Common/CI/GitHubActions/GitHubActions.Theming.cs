// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Common.Execution.Theming;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;

namespace Nuke.Common.CI.GitHubActions;

public partial class GitHubActions
{
    internal override IHostTheme Theme => AnsiConsoleHostTheme.Default256AnsiColorTheme;

    protected internal override IDisposable WriteBlock(string text)
    {
        return DelegateDisposable.CreateBracket(
            () => Group(text),
            () => EndGroup(text));
    }

    protected internal override void ReportWarning(string text, string details = null)
    {
        WriteWarning(text);
    }

    protected internal override void ReportError(string text, string details = null)
    {
        WriteError(text);
    }

    protected internal override bool FilterMessage(string message)
    {
        if (!message.StartsWith("::") && !message.StartsWith("##["))
            return false;

        Console.WriteLine(message);
        return true;
    }

    public void Group(string group)
    {
        WriteCommand("group", group);
    }

    public void EndGroup(string group)
    {
        WriteCommand("endgroup", group);
    }

    public void WriteDebug(string message)
    {
        WriteCommand("debug", message);
    }

    public void WriteWarning(string message)
    {
        WriteCommand("warning", message);
    }

    public void WriteError(string message)
    {
        WriteCommand("error", message);
    }

    public void WriteCommand(
        string command,
        string message = null,
        Configure<Dictionary<string, object>> dictionaryConfigurator = null)
    {
        var parameters = dictionaryConfigurator.InvokeSafe(new Dictionary<string, object>())
            .Select(x => $"{x.Key}={EscapeProperty(x.Value.ToString())}")
            .JoinCommaSpace();

        Console.WriteLine(parameters.IsNullOrEmpty()
            ? $"::{command}::{EscapeData(message)}"
            : $"::{command} {parameters}::{EscapeData(message)}");
    }

    private string EscapeData([CanBeNull] string data)
    {
        return data?
            .Replace("%", "%25")
            .Replace("\r", "%0D")
            .Replace("\n", "%0A");
    }

    private string EscapeProperty(string value)
    {
        return value
            .Replace("%", "%25")
            .Replace("\r", "%0D")
            .Replace("\n", "%0A")
            .Replace(":", "%3A")
            .Replace(",", "%2C");
    }
}
