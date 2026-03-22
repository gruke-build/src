// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

namespace Nuke.Components.GitLab;

public static class QueryParameters
{
    public static (string, object) Sort(string type)
    {
        return ("sort", type);
    }

    public static (string, object) OrderBy(string ordering)
    {
        return ("order_by", ordering);
    }
}
