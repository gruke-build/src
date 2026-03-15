// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.CI.TravisCI;

/// <summary>
/// Interface according to the <a href="https://docs.travis-ci.com/user/environment-variables/">official website</a>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public partial class TravisCI : Host, IBuildServer, IEnvironment<TravisCI>
{
    public static string EnvironmentVariablePrefix => "TRAVIS";

    public new static TravisCI Instance => Host.Instance as TravisCI;

    [UsedImplicitly]
    internal static bool IsRunningTravisCI => EnvironmentInfo.HasVariable("TRAVIS");

    internal TravisCI()
    {
        
    }

    string IBuildServer.Branch => Branch;
    string IBuildServer.Commit => Commit;

    public bool Ci => EnvironmentInfo.GetVariable<bool>("CI");
    public bool ContinousIntegration => EnvironmentInfo.GetVariable<bool>("CONTINUOUS_INTEGRATION");

    /// <summary>
    /// Whether you have defined any encrypted variables, including variables defined in the Repository Settings.
    /// </summary>
    public bool SecureEnvVars => IEnvironment<TravisCI>.Get<bool>("SECURE_ENV_VARS");

    /// <summary>
    /// For push builds, or builds not triggered by a pull request, this is the name of the branch.
    /// For builds triggered by a pull request this is the name of the branch targeted by the pull request.
    /// For builds triggered by a tag, this is the same as the name of the tag (<c>TRAVIS_TAG</c>).
    /// </summary>
    public string Branch => IEnvironment<TravisCI>.Get("BRANCH");

    /// <summary>
    /// The absolute path to the directory where the repository being built has been copied on the worker.
    /// </summary>
    public string BuildDir => IEnvironment<TravisCI>.Get("BUILD_DIR");

    /// <summary>
    ///  The id of the current build that Travis CI uses internally.
    /// </summary>
    public long BuildId => IEnvironment<TravisCI>.Get<long>("BUILD_ID");

    /// <summary>
    /// The number of the current build (for example, “4”).
    /// </summary>
    public long BuildNumber => IEnvironment<TravisCI>.Get<long>("BUILD_NUMBER");

    /// <summary>
    /// The commit that the current build is testing.
    /// </summary>
    public string Commit => IEnvironment<TravisCI>.Get("COMMIT");

    /// <summary>
    /// The commit subject and body, unwrapped.
    /// </summary>
    public string CommitMessage => IEnvironment<TravisCI>.Get("COMMIT_MESSAGE");

    /// <summary>
    /// The range of commits that were included in the push or pull request. (Note that this is empty for builds triggered by the initial commit of a new branch.)
    /// </summary>
    public string CommitRange => IEnvironment<TravisCI>.Get("COMMIT_RANGE");

    /// <summary>
    /// Indicates how the build was triggered.
    /// </summary>
    public TravisCIEventType EventType => IEnvironment<TravisCI>.Get<TravisCIEventType>("EVENT_TYPE");

    /// <summary>
    /// The id of the current job that Travis CI uses internally.
    /// </summary>
    public long JobId => IEnvironment<TravisCI>.Get<long>("JOB_ID");

    /// <summary>
    /// The number of the current job (for example, “4.1”).
    /// </summary>
    [NoConvert] public string JobNumber => IEnvironment<TravisCI>.Get("JOB_NUMBER");

    /// <summary>
    /// On multi-OS builds, this value indicates the platform the job is running on. Values are <c>linux</c> and <c>osx</c> currently, to be extended in the future.
    /// </summary>
    public string OsName => IEnvironment<TravisCI>.Get("OS_NAME");

    /// <summary>
    /// <c>TRAVIS_PULL_REQUEST</c> is set to the pull request number if the current job is a pull request build, or <c>false</c> if it’s not.
    /// </summary>
    [NoConvert] public string PullRequest => IEnvironment<TravisCI>.Get("PULL_REQUEST");

    /// <summary>
    /// If the current job is a pull request, the name of the branch from which the PR originated.
    /// If the current job is a push build, this variable is empty(<c>""</c>).
    /// </summary>
    public string PullRequestBranch => IEnvironment<TravisCI>.Get("PULL_REQUEST_BRANCH");

    /// <summary>
    /// If the current job is a pull request, the commit SHA of the HEAD commit of the PR.
    /// If the current job is a push build, this variable is empty(<c>""</c>).
    /// </summary>
    public string PullRequestSha => IEnvironment<TravisCI>.Get("PULL_REQUEST_SHA");

    /// <summary>
    /// If the current job is a pull request, the slug (in the form <c>owner_name/repo_name</c>) of the repository from which the PR originated.
    /// If the current job is a push build, this variable is empty(<c>""</c>).
    /// </summary>
    public string PullRequestSlug => IEnvironment<TravisCI>.Get("PULL_REQUEST_SLUG");

    /// <summary>
    /// The slug (in form: <c>owner_name/repo_name</c>) of the repository currently being built.
    /// </summary>
    public string RepoSlug => IEnvironment<TravisCI>.Get("REPO_SLUG");

    /// <summary>
    /// <c>true</c> or <c>false</c> based on whether sudo is enabled.
    /// </summary>
    public bool Sudo => IEnvironment<TravisCI>.Get<bool>("SUDO");

    /// <summary>
    /// Is set to <em>0</em> if the build is successful and <em>1</em> if the build is broken.
    /// </summary>
    [CanBeNull] public string TestResult => IEnvironment<TravisCI>.Get("TEST_RESULT");

    /// <summary>
    /// If the current build is for a git tag, this variable is set to the tag’s name.
    /// </summary>
    public string Tag => IEnvironment<TravisCI>.Get("TAG");

    [CanBeNull] public string DartVersion => IEnvironment<TravisCI>.Get("DARTVersion");
    [CanBeNull] public string GoVersion => IEnvironment<TravisCI>.Get("GOVersion");
    [CanBeNull] public string HaxeVersion => IEnvironment<TravisCI>.Get("HAXEVersion");
    [CanBeNull] public string JdkVersion => IEnvironment<TravisCI>.Get("JDKVersion");
    [CanBeNull] public string JuliaVersion => IEnvironment<TravisCI>.Get("JULIAVersion");
    [CanBeNull] public string NodeVersion => IEnvironment<TravisCI>.Get("NODEVersion");
    [CanBeNull] public string OtpRelease => IEnvironment<TravisCI>.Get("OTP_RELEASE");
    [CanBeNull] public string PerlVersion => IEnvironment<TravisCI>.Get("PERLVersion");
    [CanBeNull] public string PhpVersion => IEnvironment<TravisCI>.Get("PHPVersion");
    [CanBeNull] public string PythonVersion => IEnvironment<TravisCI>.Get("PYTHONVersion");
    [CanBeNull] public string RVersion => IEnvironment<TravisCI>.Get("RVersion");
    [CanBeNull] public string RubyVersion => IEnvironment<TravisCI>.Get("RUBYVersion");
    [CanBeNull] public string RustVersion => IEnvironment<TravisCI>.Get("RUSTVersion");
    [CanBeNull] public string ScalaVersion => IEnvironment<TravisCI>.Get("SCALAVersion");
    [CanBeNull] public string XCodeSdk => IEnvironment<TravisCI>.Get("XCODE_SDK");
    [CanBeNull] public string XCodeScheme => IEnvironment<TravisCI>.Get("XCODE_SCHEME");
    [CanBeNull] public string XCodeProject => IEnvironment<TravisCI>.Get("XCODE_PROJECT");
    [CanBeNull] public string XCodeWorkspace => IEnvironment<TravisCI>.Get("XCODE_WORKSPACE");
}
