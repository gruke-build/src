// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Discord;
using Discord.Webhook;
using Nuke.Common;
using Nuke.Common.ChangeLog;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Utilities;
using Nuke.Components;
using static Nuke.Common.Tools.Git.GitTasks;

partial class Build
{
    Target Announce => _ => _
        .DependsOn(ReleaseImage)
        .WhenSkipped(DependencyBehavior.Skip)
        .TriggeredBy<IPublish>()
        .OnlyWhenStatic(() => GitRepository.IsOnMasterBranch);

    IEnumerable<string> ChangelogSectionNotes => ChangelogTasks.ExtractChangelogSectionNotes(From<IHazChangelog>().ChangelogFile);

    string AnnouncementTitle => $"GRUKE {MajorMinorPatchVersion} RELEASED!";
    string AnnouncementLink => $"https://nuget.org/packages/GreemDev.Nuke/{MajorMinorPatchVersion}";
    uint AnnouncementColor => 0x00ACC1;

    string AnnouncementThumbnailUrl =>
        (GitVersion.Major, GitVersion.Minor, GitVersion.Patch) switch
        {
            (_, 0, 0) => "https://em-content.zobj.net/thumbs/320/apple/325/rocket_1f680.png",
            (_, _, 0) => "https://em-content.zobj.net/thumbs/320/apple/325/wrapped-gift_1f381.png",
            (_, _, _) => "https://em-content.zobj.net/thumbs/320/apple/325/package_1f4e6.png"
        };

    string AnnouncementComparisonUrl => GitRepository.GitHub.GetCompareTagsUrl(MajorMinorPatchVersion, $"{MajorMinorPatchVersion}^");

    string AnnouncementReleaseNotes =>
        new StringBuilder()
            .AppendLine("*Release Notes*")
            .AppendLine(ChangelogSectionNotes.Join('\n'))
            .ToString();

    (string CommitsText, IReadOnlyCollection<string> NotableCommmitters) AnnouncementGitInfo
    {
        get
        {
            var committers = Git($"log {MajorMinorPatchVersion}^..{MajorMinorPatchVersion} --pretty=tformat:%an", logInvocation: false,
                logOutput: false);
            var commitsText = $"{committers.Count} {(committers.Count == 1 ? "commit" : "commits")}";
            var notableCommitters = committers
                .Select(x => x.Text)
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .Where(x => x is not "Matthias Koch" and not "GreemDev").ToList();
            return (commitsText, notableCommitters);
        }
    }

    // Server settings | Apps | Integrations | Webhooks | NUKE
    [Parameter("the Discord webhook that should receive release notification embeds")] [Secret] readonly string DiscordWebhook;

    // either null, 'everyone'/'@everyone' or a role ID
    [Parameter("a role ID or @everyone")] readonly string AnnouncementPing;

    Target AnnounceDiscord => _ => _
        .TriggeredBy(Announce)
        .ProceedAfterFailure()
        .Requires(() => DiscordWebhook)
        .Executes(async () =>
        {
            var webhookClient = new DiscordWebhookClient(DiscordWebhook);
            using var fileAttachment = new FileAttachment(ReleaseImageFile);

            await webhookClient.SendFileAsync(fileAttachment,
                text: AnnouncementPing is not null 
                    ? AnnouncementPing.EqualsOrdinalIgnoreCase("everyone") ||
                      AnnouncementPing.EqualsOrdinalIgnoreCase("@everyone")
                        ? "@everyone"
                        : ulong.TryParse(AnnouncementPing, out var id)
                            ? MentionUtils.MentionRole(id)
                            : null
                    : null,
                username: "GRUKE Release",
                avatarUrl: "https://github.com/gruke-build/src/blob/develop/images/icon-social.png?raw=true",
                embeds:
                [
                    new EmbedBuilder()
                        .WithTitle(AnnouncementTitle)
                        .WithUrl(AnnouncementLink)
                        .WithColor(AnnouncementColor)
                        .WithThumbnailUrl(AnnouncementThumbnailUrl)
                        .WithImageUrl(fileAttachment.GetAttachmentUrl())
                        .WithDescription(new StringBuilder()
                            .Append($"This new release includes *[{AnnouncementGitInfo.CommitsText}]({AnnouncementComparisonUrl})*")
                            .AppendLine(AnnouncementGitInfo.NotableCommmitters.Count > 0
                                ? $" with notable contributions from {AnnouncementGitInfo.NotableCommmitters.JoinCommaAnd()}. A round of applause for them! 👏"
                                : ". No contributions this time. 😅")
                            .AppendLine()
                            .AppendLine("Remember that you can call `gruke :update` to update your builds! 💡")
                            .AppendLine()
                            .AppendLine(AnnouncementReleaseNotes).ToString()
                            .Replace("*", "**")
                        ).Build()
                ]);
        });
}
