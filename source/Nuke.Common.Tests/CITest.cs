// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using JetBrains.Annotations;
using Nuke.Common.CI;
using Nuke.Common.CI.AppVeyor;
using Nuke.Common.CI.AzurePipelines;
using Nuke.Common.CI.Bitrise;
using Nuke.Common.CI.ForgejoActions;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI.GitLab;
using Nuke.Common.CI.Jenkins;
using Nuke.Common.CI.TeamCity;
using Nuke.Common.CI.TravisCI;
using Nuke.Common.CI.WoodpeckerCI;
using Xunit;

namespace Nuke.Common.Tests;

public class CITest
{
    [CITheory(typeof(AppVeyor))]
    [MemberData(nameof(Properties), typeof(AppVeyor))]
    public void TestAppVeyor(PropertyInfo property, AppVeyor instance)
    {
        AssertProperty(instance, property);
    }

    [CITheory(typeof(Bitrise))]
    [MemberData(nameof(Properties), typeof(Bitrise))]
    public void TestBitrise(PropertyInfo property, Bitrise instance)
    {
        AssertProperty(instance, property);
    }

    [CITheory(typeof(TeamCity))]
    [MemberData(nameof(Properties), typeof(TeamCity))]
    public void TestTeamCity(PropertyInfo property, TeamCity instance)
    {
        AssertProperty(instance, property);
    }

    [CITheory(typeof(AzurePipelines))]
    [MemberData(nameof(Properties), typeof(AzurePipelines))]
    public void TestAzureDevOps(PropertyInfo property, AzurePipelines instance)
    {
        AssertProperty(instance, property);
    }

    [CITheory(typeof(Jenkins))]
    [MemberData(nameof(Properties), typeof(Jenkins))]
    public void TestJenkins(PropertyInfo property, Jenkins instance)
    {
        AssertProperty(instance, property);
    }

    [CITheory(typeof(TravisCI))]
    [MemberData(nameof(Properties), typeof(TravisCI))]
    public void TestTravisCI(PropertyInfo property, TravisCI instance)
    {
        AssertProperty(instance, property);
        Assert.True(instance.Ci);
        Assert.True(instance.ContinousIntegration);
    }

    [CITheory(typeof(ForgejoActions))]
    [MemberData(nameof(Properties), typeof(ForgejoActions))]
    public void TestForgejoActions(PropertyInfo property, ForgejoActions instance)
    {
        AssertProperty(instance, property);
        Assert.True(instance.Ci);
    }

    [CITheory(typeof(GitLab))]
    [MemberData(nameof(Properties), typeof(GitLab))]
    public void TestGitLab(PropertyInfo property, GitLab instance)
    {
        AssertProperty(instance, property);
        Assert.True(instance.Ci);
    }

    [CITheory(typeof(GitHubActions))]
    [MemberData(nameof(Properties), typeof(GitHubActions))]
    public void TestGitHubActions(PropertyInfo property, GitHubActions instance)
    {
        AssertProperty(instance, property);
        Assert.True(instance.Ci);
    }

    [CITheory(typeof(WoodpeckerCI))]
    [MemberData(nameof(Properties), typeof(WoodpeckerCI))]
    public void TestWoodpeckerCI(PropertyInfo property, WoodpeckerCI instance)
    {
        AssertProperty(instance, property);
    }

    public static IEnumerable<object[]> Properties(Type type)
    {
        var instance = CreateInstance(type);

        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(x => new[] { x, instance }).ToArray();
    }

    private static void AssertProperty(object instance, PropertyInfo property)
    {
        if (property.GetCustomAttribute<NoValueCheckAttribute>() != null)
            return;

        object value;
        try
        {
            value = property.GetValue(instance);
        }
        catch (TargetInvocationException exception)
        {
            throw exception.InnerException.NotNull();
        }

        if (property.GetCustomAttribute<NoValueCheckOnPlatformAttribute>()?.ShouldSkip() ?? false)
            return;

        if (property.GetCustomAttribute<CanBeNullAttribute>() == null)
            value.Should().NotBeNull("property attributes indicate this should be treated as non-null");
        else if (property.PropertyType != typeof(string) &&
                 property.PropertyType.IsGenericType &&
                 property.PropertyType.IsAssignableTo(typeof(Nullable<>).MakeGenericType(property.PropertyType.GenericTypeArguments[0]))
                )
            Nullable.GetUnderlyingType(property.PropertyType).Should().NotBeNull();

        if (value is not string strValue || property.GetCustomAttribute<NoConvertAttribute>() != null)
            return;

        const string errorString = "a 'string'-typed property should not be directly parseable as another type. "
                                   + "'{0}' can be parsed as a {1}; as such its property type should be updated to match, "
                                   + "to make the API cleaner.";

        bool.TryParse(strValue, out _).Should().BeFalse(errorString, strValue, "boolean");
        long.TryParse(strValue, out _).Should().BeFalse(errorString, strValue, "long");
        decimal.TryParse(strValue, out _).Should().BeFalse(errorString, strValue, "decimal");
        DateTime.TryParse(strValue, out _).Should().BeFalse(errorString, strValue, nameof(DateTime));
        TimeSpan.TryParse(strValue, out _).Should().BeFalse(errorString, strValue, nameof(TimeSpan));
        Guid.TryParse(strValue, out _).Should().BeFalse(errorString, strValue, nameof(Guid));
    }

    private static object CreateInstance(Type type)
    {
        var bindingFlags = BindingFlags.CreateInstance | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.OptionalParamBinding;
        return Activator.CreateInstance(type, bindingFlags, binder: null, args: [], culture: CultureInfo.CurrentCulture);
    }

    private static bool IsRunning(Type type)
    {
        var property = type.GetProperty($"IsRunning{type.Name}", BindingFlags.NonPublic | BindingFlags.Static).NotNull();
        return (bool)property.GetValue(obj: null);
    }

    private class CITheoryAttribute : TheoryAttribute
    {
        private readonly Type _type;

        public CITheoryAttribute(Type type)
        {
            _type = type;
        }

        public override string Skip => !IsRunning(_type) ? $"Only applies to {_type.Name}." : null;
    }
}
