// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using Nuke.Build.CICD;

namespace Nuke.Common.CI.Bitrise;

/// <summary>
/// Interface according to the <a href="https://docs.bitrise.io/en/bitrise-ci/references/available-environment-variables.html">official website</a>.
/// </summary>
[PublicAPI]
[CI]
[ExcludeFromCodeCoverage]
public class Bitrise : Host, IBuildServer, IEnvironment<Bitrise>
{
    public static string EnvironmentVariablePrefix => "BITRISE";

    public new static Bitrise Instance => Host.Instance as Bitrise;

    [UsedImplicitly]
    internal static bool IsRunningBitrise => IEnvironment<Bitrise>.Has("IO");

    private static DateTime ConvertUnixTimestamp(long timestamp)
    {
        return new DateTime(year: 1970, month: 1, day: 1, hour: 0, minute: 0, second: 0, kind: DateTimeKind.Utc)
            .AddSeconds(timestamp)
            .ToLocalTime();
    }

    internal Bitrise()
    {
    }

    string IBuildServer.Branch => GitBranch;
    string IBuildServer.Commit => GitCommit;

    public string BuildUrl => IEnvironment<Bitrise>.Get("BUILD_URL");
    public long BuildNumber => IEnvironment<Bitrise>.Get<long>("BUILD_NUMBER");
    public string AppTitle => IEnvironment<Bitrise>.Get("APP_TITLE");
    public string AppUrl => IEnvironment<Bitrise>.Get("APP_URL");
    [NoConvert] public string AppSlug => IEnvironment<Bitrise>.Get("APP_SLUG");
    [NoConvert] public string BuildSlug => IEnvironment<Bitrise>.Get("BUILD_SLUG");
    public DateTime BuildTriggerTimestamp => ConvertUnixTimestamp(IEnvironment<Bitrise>.Get<long>("BUILD_TRIGGER_TIMESTAMP"));
    public string RepositoryUrl => EnvironmentInfo.GetVariable("GIT_REPOSITORY_URL");
    public string GitBranch => IEnvironment<Bitrise>.Get("GIT_BRANCH");
    [CanBeNull] public string GitTag => IEnvironment<Bitrise>.Get("GIT_TAG");
    [CanBeNull] public string GitCommit => IEnvironment<Bitrise>.Get("GIT_COMMIT");
    [CanBeNull] public string GitMessage => IEnvironment<Bitrise>.Get("GIT_MESSAGE");
    [CanBeNull] public long? PullRequest => IEnvironment<Bitrise>.Get<long?>("PULL_REQUEST");
    [CanBeNull] public string ProvisionUrl => IEnvironment<Bitrise>.Get("PROVISION_URL");
    [CanBeNull] public string CertificateUrl => IEnvironment<Bitrise>.Get("CERTIFICATE_URL");
    [CanBeNull] public string CertificatePassphrase => IEnvironment<Bitrise>.Get("CERTIFICATE_PASSPHRASE");
}
