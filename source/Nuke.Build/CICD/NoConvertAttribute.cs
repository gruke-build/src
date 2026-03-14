// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Linq;

namespace Nuke.Common.CI;

[AttributeUsage(AttributeTargets.Property)]
public class NoConvertAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
internal class NoValueCheckAttribute : Attribute;
