// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;

namespace Nuke.Common.Utilities;

public static class DateTimeUtilities
{
    extension(DateTime)
    {
        /// <summary>
        /// Converts a UNIX timestamp into a .NET <see cref="DateTime"/> by constructing a new <see cref="DateTime"/>
        /// starting at the Unix epoch (the very first second of Jan 1 1970) in UTC, then adding the timestamp to it directly as seconds.
        /// </summary>
        /// <param name="ts">the Unix timestamp</param>
        /// <returns>A <see cref="DateTime"/> representing the passed Unix timestamp.</returns>
        public static DateTime FromUnixTimestamp(long ts)
        {
            return new DateTime(year: 1970, month: 1, day: 1, hour: 0, minute: 0, second: 0, kind: DateTimeKind.Utc)
                .AddSeconds(ts);
        }
    }
}
