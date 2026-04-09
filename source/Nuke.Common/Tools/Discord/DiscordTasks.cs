// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System.Net.Http;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Net;

namespace Nuke.Common.Tools.Discord;

[PublicAPI]
public static class DiscordTasks
{
    public static void SendDiscordMessage(Configure<DiscordMessage> configurator, string webhook)
    {
        SendDiscordMessageAsync(configurator, webhook).Wait();
    }

    public static async Task SendDiscordMessageAsync(Configure<DiscordMessage> configurator, string webhook)
    {
        var message = configurator(new DiscordMessage());

        var response = await HttpClientProxy.Shared.CreateRequest(HttpMethod.Post, webhook)
            .WithJsonContent(message)
            .GetResponseAsync();

        response.AssertSuccessfulStatusCode();
    }
}
