// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using JetBrains.Annotations;

namespace Nuke.Common.CI.ForgejoActions;

/// <summary>
///     The constants on this class and their accompanying documentation were sourced from the <a href="https://codeberg.org/actions/meta">official Codeberg actions/meta repository for their Forgejo Actions.</a>
///     <br/><br/><br/>
///     Codeberg has two hosted CI/CD solutions:
///     <a href="https://docs.codeberg.org/ci/#using-codeberg's-instance-of-woodpecker-ci">Woodpecker CI</a>
///     and
///     <a href="https://forgejo.org/docs/next/user/actions/overview/">Forgejo Actions</a>.
///     <br/>
///     This class is related to Forgejo Actions hosted by Codeberg,
///     but you can easily <a href="https://forgejo.org/docs/next/admin/actions/runner-installation/">set up the runner on your own machine</a>.
///     <br/><br/>
///     The Hosted Forgejo Actions is in open beta. It has worked reliably for a several months, and the Forgejo runner matured a lot in this time.
///     <br/>
///     Currently, the runner has less capacity than the hosted Woodpecker CI. Capacity and offer expansion is expected through the next year;
///     consider <a href="https://donate.codeberg.org/">chipping in financially</a>, if you can.
///
///     The constants in this class are runners available on Codeberg.
///     The specs in the in-code docs are guidelines for how many resources builds are allowed to take at most, but Codeberg relies on "fair use".
///     Let's not abuse what is given to us so generously; with no predatory ads, monetization policies, or data harvesting.
///
///     <br/><br/><br/>
///     Rules and conditions
///     <br/>
///     In short:<br/>
///         - project must be <b>public</b> (<b>and available under a free/libre license</b> as per the <a href="https://codeberg.org/Codeberg/org/src/branch/main/TermsOfUse.md">Terms of Use</a>)<br/>
///         - be excellent to each other: <b>Don't jam the queue</b> so other projects can also benefit from the offer<br/>
///         - only use the <b>resources you really need</b><br/><br/>
///
///     Running CI/CD pipelines can use significant amounts of energy.
///     As much as it is tempting to have green checkmarks everywhere, running the jobs costs real money and has environmental costs.
///     <br/><br/>
///     Unlike other giant platforms, Codeberg does not encourage you to write "heavy" pipelines, then charge you for the cost later.
///     You are expected to carefully consider the costs and benefits from your pipelines and reduce CI/CD usage
///     to a minimum amount necessary to guarantee consistent quality for your projects.
///     <br/><br/>
///     Codeberg invites you to participate in discussions with them on how to optimize pipelines and save unnecessary runs where possible.
/// </summary>
[PublicAPI]
public static class CodebergRunners
{
    /// <summary>
    /// Intended for very lightweight jobs like linters and other helpers.<br/><br/>
    ///     Architecture: amd64<br/>
    ///     CPU Core Count: 1<br/>
    ///     RAM: 2 GB<br/>
    ///     Runtime: 2 minutes
    /// </summary>
    public const string Tiny = "codeberg-tiny";

    /// <summary>
    ///     Architecture: amd64<br/>
    ///     CPU Core Count: 2<br/>
    ///     RAM: 4 GB<br/>
    ///     Runtime: 5 minutes
    /// </summary>
    public const string Small = "codeberg-small";

    /// <summary>
    ///     Architecture: amd64<br/>
    ///     CPU Core Count: 4<br/>
    ///     RAM: 8 GB<br/>
    ///     Runtime: 10 minutes
    /// </summary>
    public const string Medium = "codeberg-medium";

    /// <summary>
    /// Intended for very lightweight jobs like linters and other helpers.<br/><br/>
    ///     Architecture: amd64<br/>
    ///     CPU Core Count: 1<br/>
    ///     RAM: 2 GB<br/>
    ///     Runtime: 2 minutes
    /// </summary>
    /// <remarks>
    /// For each runner variant, Codeberg offers a "lazy" runner.<br/>
    /// It can generally have more delay in picking jobs, but will help Codeberg schedule jobs based on load or energy availability.<br/>
    /// If your CI jobs can wait, consider using this runner to optimize the load on our systems.<br/><br/>
    /// Codeberg aims for completing the job within 24 hours, and are considering experiments like offsite runners which only run when solar energy is available.
    /// </remarks>
    public const string TinyLazy = Tiny + "-lazy";

    /// <summary>
    ///     Architecture: amd64<br/>
    ///     CPU Core Count: 2<br/>
    ///     RAM: 4 GB<br/>
    ///     Runtime: 5 minutes
    /// </summary>
    /// <remarks>
    /// For each runner variant, Codeberg offers a "lazy" runner.<br/>
    /// It can generally have more delay in picking jobs, but will help Codeberg schedule jobs based on load or energy availability.<br/>
    /// If your CI jobs can wait, consider using this runner to optimize the load on our systems.<br/><br/>
    /// Codeberg aims for completing the job within 24 hours, and are considering experiments like offsite runners which only run when solar energy is available.
    /// </remarks>
    public const string SmallLazy = Small + "-lazy";

    /// <summary>
    ///     Architecture: amd64<br/>
    ///     CPU Core Count: 4<br/>
    ///     RAM: 8 GB<br/>
    ///     Runtime: 10 minutes
    /// </summary>
    /// <remarks>
    /// For each runner variant, Codeberg offers a "lazy" runner.<br/>
    /// It can generally have more delay in picking jobs, but will help Codeberg schedule jobs based on load or energy availability.<br/>
    /// If your CI jobs can wait, consider using this runner to optimize the load on our systems.<br/><br/>
    /// Codeberg aims for completing the job within 24 hours, and are considering experiments like offsite runners which only run when solar energy is available.
    /// </remarks>
    public const string MediumLazy = Medium + "-lazy";
}
