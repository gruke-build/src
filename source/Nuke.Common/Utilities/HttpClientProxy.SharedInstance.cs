// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.Utilities.Net;
using Serilog;

namespace Nuke.Common.Utilities;

public static class HttpClientProxySharedInstance
{
    private static readonly Lazy<HttpClientProxy> s_shared = Lazy.Create(() => new HttpClientProxy(
            new HttpClient
            {
                DefaultRequestHeaders =
                {
                    UserAgent =
                    {
                        new ProductInfoHeaderValue("GRUKE", typeof(INukeBuild).Assembly.GetVersionText())
                    }
                }
            },
            (fmt, args, extraLines, _) =>
            {
#pragma warning disable CA2254
                Log.Information(fmt, args);
                extraLines.ForEach(line => Log.Error(line));
#pragma warning restore CA2254
            }
        )
    );

    extension(HttpClientProxy)
    {
        public static HttpClientProxy Shared => s_shared.Value;
    }
}
