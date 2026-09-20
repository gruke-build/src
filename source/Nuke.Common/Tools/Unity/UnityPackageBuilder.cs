// Copyright 2026 Mabel Amber
// Copyright 2026 Maintainers of GRUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

// Original: https://git.weatherelectric.xyz/MabelAmber/UnityPackager

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using JetBrains.Annotations;
using Nuke.Common.IO;

namespace Nuke.Common.Tools.Unity;

[PublicAPI]
public class UnityPackageBuilder
{
    /// <summary>
    /// Local directory containing the assets and their .meta files
    /// </summary>
    public AbsolutePath SourceDirectory { get; private set; }
    /// <summary>
    /// Where those assets live in a Unity project, e.g. "Assets/Shaders"
    /// </summary>
    public string UnityRoot { get; private set; }
    /// <summary>
    /// Package file output path
    /// </summary>
    public AbsolutePath OutputPath { get; private set; }

    /// <summary>
    /// Local directory containing the assets and their .meta files
    /// </summary>
    public UnityPackageBuilder WithSourceDirectory(AbsolutePath sourceDir)
    {
        SourceDirectory = sourceDir;
        return this;
    }

    /// <summary>
    /// Where those assets live in a Unity project, e.g. "Assets/Shaders"
    /// </summary>
    public UnityPackageBuilder WithUnityRoot(string unityRoot)
    {
        UnityRoot = unityRoot;
        return this;
    }

    /// <summary>
    /// Package file output path
    /// </summary>
    /// <remarks><c>.unitypackage</c> is appended to the input automatically</remarks>
    public UnityPackageBuilder WithOutputPath(AbsolutePath outputPath)
    {
        OutputPath = outputPath.Extension is PackageExtension 
            ? outputPath
            : outputPath + PackageExtension;

        return this;
    }

    private sealed record Asset(string SourcePath, string UnityPath, string Guid, bool IsDirectory);

    private const string PackageExtension = ".unitypackage";

    internal void Assertion()
    {
        SourceDirectory.NotNull("Unity package builder requires a source directory.");
        UnityRoot.NotNull("Unity package builder requires a Unity root, which is where the assets should live in a Unity project.");
        OutputPath.NotNull("Unity package builder requires an output path.");
    }

    /// <summary>
    ///     Perform the package-building operation on the inputs provided.
    /// </summary>
    /// <returns>The resulting <c>.unitypackage</c>'s path.</returns>
    /// <exception cref="DirectoryNotFoundException"><see cref="SourceDirectory"/> does not exist as a directory.</exception>
    /// <exception cref="InvalidOperationException"><see cref="SourceDirectory"/> does not contain any Unity assets.</exception>
    /// <exception cref="InvalidDataException">The Unity package's contents are invalid.</exception>
    public AbsolutePath Build()
    {
        Assertion();

        if (!SourceDirectory.DirectoryExists())
            throw new DirectoryNotFoundException($"Source directory not found: {SourceDirectory}");

        var assets = CollectAssets(SourceDirectory, NormalizeUnityRoot(UnityRoot));

        if (assets.Count == 0)
            throw new InvalidOperationException($"No assets found in {SourceDirectory}");

        OutputPath.Parent?.CreateDirectory();

        var tempPath = OutputPath + ".tmp";
        try
        {
            WriteArchive(tempPath, assets);
            File.Move(tempPath, OutputPath, overwrite: true);
        }
        finally
        {
            File.Delete(tempPath);
        }

        return OutputPath;
    }

    private static List<Asset> CollectAssets(AbsolutePath sourceDir, string unityRoot)
    {
        List<Asset> assets = [];
        List<string> missingMeta = [];
        Dictionary<string, string> seenGuids = new(StringComparer.OrdinalIgnoreCase);

        IEnumerable<string> entries = Directory
            .EnumerateFileSystemEntries(sourceDir, "*", SearchOption.AllDirectories)
            .Order(StringComparer.Ordinal);

        foreach (var path in entries)
        {
            var relative = Path.GetRelativePath(sourceDir, path).Replace(oldChar: '\\', newChar: '/');

            if (relative.Split('/').Any(IsIgnored))
                continue;

            if (!TryReadGuid($"{path}.meta", out var guid))
            {
                missingMeta.Add(relative);
                continue;
            }

            if (!seenGuids.TryAdd(guid, relative))
            {
                throw new InvalidDataException(
                    $"Duplicate GUID {guid} used by '{seenGuids[guid]}' and '{relative}'.");
            }

            assets.Add(new Asset(
                SourcePath: path,
                UnityPath: $"{unityRoot.TrimEnd('/')}/{relative}",
                Guid: guid,
                IsDirectory: Directory.Exists(path)));
        }

        if (missingMeta.Count > 0)
        {
            throw new InvalidDataException(
                "These paths have no usable .meta file (missing, or no valid guid):\n  - "
                + string.Join("\n  - ", missingMeta));
        }

        return assets;
    }

    private static void WriteArchive(string path, IEnumerable<Asset> assets)
    {
        using var file = File.Create(path);
        using GZipOutputStream gzip = new(file);
        using TarOutputStream tar = new(gzip, Encoding.UTF8);

        foreach (var asset in assets)
        {
            AddDirectory(tar, asset.Guid);

            if (!asset.IsDirectory)
                AddFile(tar, $"{asset.Guid}/asset", asset.SourcePath);

            AddFile(tar, $"{asset.Guid}/asset.meta", asset.SourcePath + ".meta");
            AddBytes(tar, $"{asset.Guid}/pathname", Encoding.UTF8.GetBytes(asset.UnityPath));
        }
    }

    private static void AddDirectory(TarOutputStream tar, string name)
    {
        tar.PutNextEntry(TarEntry.CreateTarEntry(name + "/"));
        tar.CloseEntry();
    }

    private static void AddBytes(TarOutputStream tar, string name, byte[] data)
    {
        var entry = TarEntry.CreateTarEntry(name);
        entry.Size = data.Length;

        tar.PutNextEntry(entry);
        tar.Write(data, offset: 0, data.Length);
        tar.CloseEntry();
    }

    private static void AddFile(TarOutputStream tar, string name, string sourcePath)
    {
        using var source = File.OpenRead(sourcePath);

        var entry = TarEntry.CreateTarEntry(name);
        entry.Size = source.Length;

        tar.PutNextEntry(entry);
        source.CopyTo(tar);
        tar.CloseEntry();
    }

    private static bool TryReadGuid(string metaPath, [MaybeNullWhen(false)] out string guid)
    {
        guid = null;

        if (!File.Exists(metaPath))
            return false;

        foreach (var line in File.ReadLines(metaPath))
        {
            if (!line.StartsWith("guid:", StringComparison.Ordinal))
                continue;

            guid = line["guid:".Length..].Trim();
            return guid.Length == 32 && guid.All(char.IsAsciiHexDigit);
        }

        return false;
    }

    private static bool IsIgnored(string name)
    {
        return name.StartsWith('.')
               || name.EndsWith('~')
               || name.EndsWith(".meta", StringComparison.OrdinalIgnoreCase)
               || name.EndsWith(".tmp", StringComparison.OrdinalIgnoreCase)
               || name.Equals("cvs", StringComparison.OrdinalIgnoreCase);
    }

    private static string NormalizeUnityRoot(string unityRoot)
    {
        var normalized = unityRoot.Replace(oldChar: '\\', newChar: '/').Trim('/');

        if (normalized.Length == 0)
            throw new ArgumentException("UnityRootDirectory can't be empty.");

        if (normalized != "Assets"
            && !normalized.StartsWith("Assets/", StringComparison.Ordinal)
            && !normalized.StartsWith("Packages/", StringComparison.Ordinal))
        {
            Console.Error.WriteLine(
                $"warning: '{normalized}' doesn't start with Assets/ or Packages/; Unity may ignore these assets.");
        }

        return normalized;
    }
}
