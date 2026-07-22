// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Nuke.Common.IO;

/// <summary>
/// Provides a simple API for creating a macOS .app bundle directory from a few central components.
/// </summary>
[PublicAPI]
public class AppBundleBuilder(AbsolutePath outputPath)
{
    /// <summary>
    /// The app bundle's ARM64, AMD64, or <see href="https://en.wikipedia.org/wiki/Universal_binary">Universal</see> (V2) MachO executable entrypoint.
    /// </summary>
    public AbsolutePath Executable { get; private set; }

    /// <summary>
    /// The entrypoint executable's name when copied over into the bundle. Defaults to the original file's name.
    /// </summary>
    [CanBeNull]
    public string OverriddenExecutableNameInBundle { get; private set; } = null!;
    /// <summary>
    /// The .dylib files to be copied into the app bundle's <c>{output}/Contents/Frameworks</c> directory.
    /// </summary>
    public List<AbsolutePath> Frameworks { get; private set; } = new();
    /// <summary>
    /// The files to be copied into the app bundle's <c>{output}/Contents/Resources</c> directory.
    /// </summary>
    public List<AbsolutePath> Resources { get; private set; } = new();
    
    /// <summary>
    /// The app bundle's information property list. Required to create a functional app bundle directory.
    /// </summary>
    public AbsolutePath InfoPropertyList { get; private set; }
    
    /// <summary>
    /// The app bundle's compiled <see href="https://developer.apple.com/documentation/xcode/managing-assets-with-asset-catalogs/">asset catalog</see> (.car) file, to be used for displaying different app icons under different scenarios (for example, Light vs Dark).
    /// </summary>
    [CanBeNull] public AbsolutePath AssetCatalog { get; private set; }

    /// <summary>
    /// Provide an ARM64, AMD64, or <see href="https://en.wikipedia.org/wiki/Universal_binary">Universal</see> (V2) MachO binary to be used as this app bundle's entrypoint binary.
    /// </summary>
    /// <param name="path">The path to the executable.</param>
    /// <param name="overriddenName">The name to be given to the file when copied. Use this if the compiled output name is different from what is specified in the <c>Info.plist</c>.</param>
    /// <returns>The current <see cref="AppBundleBuilder"/>, for chaining convenience.</returns>
    public AppBundleBuilder WithExecutable(AbsolutePath path, [CanBeNull] string overriddenName = null)
    {
        Executable = path;
        OverriddenExecutableNameInBundle = overriddenName;
        return this;
    }
    
    /// <summary>
    /// Specify the source path of the app bundle's <c>Info.plist</c> file.
    /// </summary>
    /// <param name="path">The path to the Info.plist file.</param>
    /// <returns>The current <see cref="AppBundleBuilder"/>, for chaining convenience.</returns>
    /// <remarks>
    /// This is required. This serves the important purpose of letting macOS know what the name of the binary from <see cref="Executable"/> is named, as well as giving your app bundle an identifier.
    /// </remarks>
    public AppBundleBuilder WithInfoPropertyList(AbsolutePath path)
    {
        Assert.True(path.Extension == ".plist");
        
        InfoPropertyList = path;
        return this;
    }
    
    /// <summary>
    /// Additional <c>.dylib</c> files to be included with your app. Use this for external native dependencies, for example libSkiaSharp or libSDL3.
    /// </summary>
    /// <param name="paths">One or multiple paths to include.</param>
    /// <returns>The current <see cref="AppBundleBuilder"/>, for chaining convenience.</returns>
    public AppBundleBuilder WithFrameworks(params AbsolutePath[] paths)
    {
        Assert.True(paths.All(x => x.HasExtension(".dylib")));
        
        Frameworks.AddRange(paths);
        return this;
    }
    
    /// <summary>
    /// Remove a path from the added <see cref="Frameworks"/>.
    /// </summary>
    /// <param name="path">The path to remove.</param>
    /// <returns>The current <see cref="AppBundleBuilder"/>, for chaining convenience.</returns>
    public AppBundleBuilder RemoveFramework(AbsolutePath path)
    {
        Frameworks.Remove(path);
        return this;
    }
    
    /// <summary>
    /// Additional files to be included with your app.
    /// </summary>
    /// <param name="paths">One or multiple paths to include.</param>
    /// <returns>The current <see cref="AppBundleBuilder"/>, for chaining convenience.</returns>
    public AppBundleBuilder WithResources(params IEnumerable<AbsolutePath> paths)
    {
        Resources.AddRange(paths);
        return this;
    }
    
    /// <summary>
    /// Remove a path from the added <see cref="Resources"/>.
    /// </summary>
    /// <param name="path">The path to remove.</param>
    /// <returns>The current <see cref="AppBundleBuilder"/>, for chaining convenience.</returns>
    public AppBundleBuilder RemoveResource(AbsolutePath path)
    {
        Resources.Remove(path);
        return this;
    }
    
    /// <summary>
    /// Add an <see cref="AbsolutePath"/> to <see cref="Resources"/>, referencing an "Apple icon image" (.icns) file.
    /// </summary>
    /// <param name="path">The path to the .icns file.</param>
    /// <returns>The current <see cref="AppBundleBuilder"/>, for chaining convenience.</returns>
    public AppBundleBuilder WithIconSet(AbsolutePath path)
    {
        Assert.True(path.Extension == ".icns");
        
        Resources.Add(path);
        return this;
    }
    
    /// <summary>
    /// Add an <see cref="AbsolutePath"/> to <see cref="Resources"/>, referencing an "Asset catalog" (Assets.car) file.<br/>
    /// Since macOS ignores the file when it does not have the name "Assets.car", this builder will rename it for you.
    /// </summary>
    /// <param name="path">The path to the .car file.</param>
    /// <returns>The current <see cref="AppBundleBuilder"/>, for chaining convenience.</returns>
    public AppBundleBuilder WithAssetCatalog(AbsolutePath path)
    {
        Assert.True(path.Extension == ".car");
        
        Resources.Add(path);
        return this;
    }

    /// <summary>
    /// Constructs the final app bundle in the specified <see cref="outputPath"/>. The path is cleaned before the operation begins.
    /// </summary>
    /// <param name="onDylibCopied">Invoked every time a .dylib from <see cref="Frameworks"/> is copied into the output. You can use this for codesigning.</param>
    /// <param name="onExecutableCopied">Invoked when the executable from <see cref="Executable"/> is copied, after the name from <see cref="OverriddenExecutableNameInBundle"/> has been applied (if present). You can use this for codesigning.</param>
    /// <remarks>
    /// This method will throw if you have not provided all necessary app bundle components (Info.plist + executable).
    /// Do note that your resulting app bundle still may not work. In that case, please be sure you have included all native libraries that are output alongside your binary.
    /// </remarks>
    public void Build([CanBeNull] Action<AbsolutePath> onExecutableCopied = null, [CanBeNull] Action<AbsolutePath> onDylibCopied = null)
    {
        Executable.NotNull("Cannot create an app bundle with no executable.");
        InfoPropertyList.NotNull("Cannot create an app bundle with no information property list.");
        
        outputPath.CreateOrCleanDirectory();
        var contents = (outputPath / "Contents").CreateDirectory();
        var frameworks = (contents / "Frameworks").CreateDirectory();
        var resources = (contents / "Resources").CreateDirectory();
        var macOs = (contents / "MacOS").CreateDirectory();

        var executablePath = OverriddenExecutableNameInBundle != null 
            ? Executable.Copy(macOs / OverriddenExecutableNameInBundle)
            : Executable.CopyToDirectory(macOs);
        
        onExecutableCopied?.Invoke(executablePath);

        foreach (var dylib in Frameworks)
        {
            dylib.CopyToDirectory(frameworks);
            onDylibCopied?.Invoke(dylib);
        }
        
        InfoPropertyList.CopyToDirectory(contents);

        foreach (var resource in Resources)
        {
            resource.CopyToDirectory(resources);
        }

        AssetCatalog?.Copy(resources / "Assets.car");

        (contents / "PkgInfo").WriteAllBytes("APPL????"u8.ToArray());
    }
}
