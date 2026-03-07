// Copyright 2023 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using Serilog;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using static Nuke.Common.IO.HttpTasks;

partial class Build
{
    [LatestGitHubRelease("JetBrains/JetBrainsMono")]
    readonly string JetBrainsMonoVersion;

    string[] FontDownloadUrls =>
        new[]
        {
            "https://github.com/googlefonts/roboto/releases/latest/download/roboto-unhinted.zip",
            $"https://github.com/JetBrains/JetBrainsMono/releases/download/v{JetBrainsMonoVersion}/JetBrainsMono-{JetBrainsMonoVersion}.zip"
        };

    AbsolutePath FontDirectory => TemporaryDirectory / "fonts";
    IReadOnlyCollection<AbsolutePath> FontArchives => FontDirectory.GlobFiles("*.*");
    IReadOnlyCollection<AbsolutePath> FontFiles => FontDirectory.GlobFiles("**/[!\\.]*.ttf");
    readonly FontCollection FontCollection = new();

    Target InstallFonts => _ => _
        .Executes(() =>
        {
            FontDownloadUrls.ForEach(x => HttpDownloadFile(x, FontDirectory / new Uri(x).Segments.Last()));
            FontArchives.ForEach(x => x.UncompressTo(FontDirectory / x.NameWithoutExtension));

            FontFiles.ForEach(x =>
            {
                FontCollection.Add(x);
                Log.Information("Installed font {Font}", x.Name);
            });
        });

    AbsolutePath WatermarkImageFile => RootDirectory / "images" / "logo-watermark.png";
    AbsolutePath ReleaseImageFile => TemporaryDirectory / "release-image.png";

    Target ReleaseImage => _ => _
        .DependsOn(InstallFonts)
        .Executes(() =>
        {
            const float logoScaling = 0.37f;
            var logo = Image.Load(WatermarkImageFile);
            logo.Mutate(x => x.Resize((int)(logo.Width * logoScaling), (int)(logo.Height * logoScaling)));

            var thinFont = FontCollection.Families.Single(x => x.Name == "JetBrains Mono Thin");
            var boldFont = FontCollection.Families.Single(x => x.Name == "JetBrains Mono ExtraBold");

            const int width = 1200;
            const int height = 675;
            var image = new Image<Rgba64>(width: width, height: height);

            var headerTextOptions = new RichTextOptions(thinFont.CreateFont(100))
                                    {
                                        Origin = new PointF(image.Width / 2f, image.Height / 2f - 120),
                                        HorizontalAlignment = HorizontalAlignment.Center,
                                        VerticalAlignment = VerticalAlignment.Center
                                    };

            var headerText = "New Release".ToUpperInvariant();
            var headerRect = TextMeasurer.MeasureBounds(headerText, headerTextOptions);
            var gradientBrush = new LinearGradientBrush(
                new PointF(headerRect.Left, headerRect.Top),
                new PointF(headerRect.Right, headerRect.Bottom),
                GradientRepetitionMode.Repeat,
                GetRainbowGradientStops().ToArray()
            );

            image.Mutate(x => x
                .BackgroundColor(color: Color.FromRgb(r: 25, g: 25, b: 25))
                .DrawImage(
                    foreground: logo,
                    backgroundLocation: new Point(image.Width / 2 - logo.Width / 2, image.Height / 2 - logo.Height / 2),
                    opacity: 0.05f)
                .DrawText(headerTextOptions, headerText, gradientBrush)
                .DrawText(
                    text: MajorMinorPatchVersion,
                    color: Color.WhiteSmoke,
                    textOptions: new RichTextOptions(boldFont.CreateFont(230))
                                 {
                                     Origin = new PointF(image.Width / 2f, image.Height / 2f + 60),
                                     HorizontalAlignment = HorizontalAlignment.Center,
                                     VerticalAlignment = VerticalAlignment.Center
                                 }));

            using var fileStream = new FileStream(ReleaseImageFile, FileMode.Create);
            image.SaveAsPng(fileStream);
        });

    private IEnumerable<ColorStop> GetRainbowGradientStops()
    {
        yield return createStop(ratio: 0.166666667f, color: 0xFE0000);
        yield return createStop(ratio: 0.333333334f, color: 0xFE8D00);
        yield return createStop(ratio: 0.500000001f, color: 0xFFEE00);
        yield return createStop(ratio: 0.666666668f, color: 0x018114);
        yield return createStop(ratio: 0.833333335f, color: 0x014CFF);
        yield return createStop(ratio: 1f, color: 0x8A018C);

        yield break;

        static ColorStop createStop(float ratio, uint color)
        {
            var red = (byte)((color >> 16) & 255);
            var green = (byte)((color >> 8) & 255);
            var blue = (byte)(color & 255);

            return new ColorStop(ratio, new Color(new Rgb24(red, green, blue)));
        }
    }
}
