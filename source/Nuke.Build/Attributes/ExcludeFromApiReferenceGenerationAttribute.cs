// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;

namespace Nuke.Common;

/// <summary>
/// Excludes the applied member from DocFX generated output.
/// </summary>
internal sealed class ExcludeFromApiReferenceGenerationAttribute : Attribute;
