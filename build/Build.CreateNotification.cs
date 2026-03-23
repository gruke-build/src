// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using JetBrains.Annotations;
using Nuke.Build.Shared;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Utilities;

public partial class Build
{
    AbsolutePath NotificationsJsonFile => RootDirectory / ".nuke" / "notifications.json";

    [UsedImplicitly]
    Target CreateNotification => _ => _
        .Executes(() =>
        {
            var notification = new Notification { Id = Guid.NewGuid() };
            Notification[] existing = [];
            if (NotificationsJsonFile.Exists())
            {
                existing = JsonSerializer.Deserialize(NotificationsJsonFile.ReadAllText(), NotificationJsonSerializer.Default.NotificationArray);
            }
            var notificationLinks = new List<Link>();

            notification.Title =
                ConsoleUtility.PromptForInput("What should the title of the notification be?", "Important information for GRUKE users");

            Body:
            notification.Text = ConsoleUtility.PromptForInput("What should the content of the notification be?");
            if (notification.Text is null)
            {
                Host.Warning("You must provide a content value; there is no default.");
                goto Body;
            }

            while (AddLink(notificationLinks))
            {
                notificationLinks.Add(PromptForLink());
            }

            notification.Links = notificationLinks.ToArray();

            NotificationsJsonFile.DeleteFile();
            NotificationsJsonFile.WriteAllText(JsonSerializer.Serialize(
                existing.Append(notification.Assertion()).ToArray(),
                NotificationJsonSerializer.Default.NotificationArray
            ));
        });

    private bool AddLink(List<Link> links)
    {
        return ConsoleUtility.PromptForChoice(
            links.Count is 0 ? "Do you wish to link to anything with this notification?" : "Do you wish to add another link to this notification?",
            (true, "Yes"),
            (false, "No")
        );
    }

    private Link PromptForLink()
    {
        Text:
        var text = ConsoleUtility.PromptForInput("What should the text prefix of the link be?");
        if (text is null)
        {
            Host.Warning("You must provide a link text value; there is no default.");
            goto Text;
        }

        Url:
        var url = ConsoleUtility.PromptForInput("What should the actual link be?");
        try
        {
            _ = new Uri(url);
        }
        catch
        {
            Host.Error("Could not be parsed as a URL. Please ensure it is correctly formatted.");
            goto Url;
        }

        return new Link { Text = text, Url = url }.Assertion();
    }
}
