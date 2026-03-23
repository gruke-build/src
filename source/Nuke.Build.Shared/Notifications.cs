// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;

namespace Nuke.Build.Shared;

[PublicAPI]
internal record Notification
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("text")] public string Text { get; set; }
    [JsonPropertyName("links")] public Link[] Links { get; set; }

    internal Notification Assertion()
    {
        Title.NotNull();
        Text.NotNull();
        Links.NotNull();
        return this;
    }
}

[PublicAPI]
internal record Link
{
    [JsonPropertyName("text")] public string Text { get; set; }
    [JsonPropertyName("url")] public string Url { get; set; }

    internal Link Assertion()
    {
        Text.NotNull();
        Url.NotNull();
        return this;
    }
}

[JsonSerializable(typeof(Notification[]))]
internal partial class NotificationJsonSerializer : JsonSerializerContext;

[PublicAPI]
internal static class NotificationFetcher
{
    public static Notification[] Cached { get; private set; } = [];

    private const string NotificationEndpoint = "https://fs.greemdev.net/fs/notifications.json";

    private static readonly AbsolutePath s_notificationDirectory = Constants.GlobalNukeDirectory / "received-notifications";

    public static async Task<Notification[]> GetNotificationsAsync()
    {
        try
        {
            var notifications = await GetNotificationsInternal();

            var newNotifications = notifications
                .Select(x => (Json: x, File: s_notificationDirectory / x.Id.ToString()))
                .Where(x => !x.File.Exists())
                .ToArray();

            foreach (var (_, file) in newNotifications)
            {
                file.TouchFile(createDirectories: true);
            }

            return newNotifications.Select(x => x.Json).ToArray();
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static async Task<Notification[]> GetNotificationsInternal()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(NotificationEndpoint).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            return null;

        // ReSharper disable once UseAwaitUsing
        // 'await using' causes this code to not be able to compile for .NET Standard 2.0.
        using var contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

        return await JsonSerializer.DeserializeAsync(contentStream, NotificationJsonSerializer.Default.NotificationArray).ConfigureAwait(false);
    }
}
