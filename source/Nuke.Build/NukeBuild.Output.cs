// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Nuke.Build.Shared;
using Nuke.Common.Utilities;
using Serilog;

namespace Nuke.Common;

partial class NukeBuild
{
    internal void WriteLogo()
    {
        if (IsInterceptorExecution)
            return;

        if (IsOutputEnabled(DefaultOutput.Logo))
            Host.WriteLogo();

        Host.Information($"NUKE Execution Engine {typeof(NukeBuild).Assembly.GetInformationalText()}");
        Host.Information();
    }

    internal IDisposable WriteTarget(string target)
    {
        bool CanCollapse() =>
            Host.GetType().GetMethod(nameof(Host.WriteBlock), ReflectionUtility.Instance | BindingFlags.DeclaredOnly) != null;

        if (IsInterceptorExecution)
            return DelegateDisposable.CreateBracket();

        if (IsOutputEnabled(DefaultOutput.TargetHeader) && !CanCollapse() ||
            IsOutputEnabled(DefaultOutput.TargetCollapse) && CanCollapse())
            return Host.WriteBlock(target);

        return DelegateDisposable.CreateBracket();
    }

    internal void WriteErrorsAndWarnings()
    {
        if (IsInterceptorExecution)
            return;

        if (IsOutputEnabled(DefaultOutput.ErrorsAndWarnings))
            Host.WriteErrorsAndWarnings();
    }

    internal void WriteTargetOutcome()
    {
        if (IsInterceptorExecution)
            return;

        if (IsOutputEnabled(DefaultOutput.TargetOutcome))
            Host.WriteTargetOutcome(this);
    }

    internal void WriteBuildOutcome()
    {
        if (IsInterceptorExecution)
            return;

        if (IsOutputEnabled(DefaultOutput.BuildOutcome))
            Host.WriteBuildOutcome(this);
    }

    internal void WriteNotifications()
    {
        if (IsInterceptorExecution)
            return;

        if (IsOutputEnabled(DefaultOutput.Notifications))
        {
            var temp = CopyArray(NotificationFetcher.Cached);

            if (temp.Length is 0)
                return;

            Host.WriteBlock("Notifications").Dispose();

            var notificationBody = new StringBuilder();

            foreach (var notification in temp)
            {
                notificationBody.AppendLine(notification.Title);
                notificationBody.AppendLine(notification.Text);
                if (notification.Links.Length is not 0)
                {
                    notificationBody.AppendLine("Links:");
                    foreach (var link in notification.Links)
                    {
                        notificationBody.AppendLine($"  - {link.Text}: {link.Url}");
                    }
                }

                var formattedText = notificationBody.ToString();
                notificationBody.Clear();

                foreach (var line in formattedText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
                {
                    Log.Information(line);
                }
            }
        }

        return;

        T[] CopyArray<T>(T[] source)
        {
            var destination = new T[source.Length];
            source.CopyTo(new Span<T>(destination));
            return destination;
        }
    }

    internal bool IsOutputEnabled(DefaultOutput output)
    {
        return !GetType().GetCustomAttributes<DisableDefaultOutputAttribute>()
            .Where(x => x.IsApplicable(this))
            .Any(x => x.DisabledOutputs.Contains(output));
    }
}
