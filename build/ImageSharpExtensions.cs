// Copyright 2026 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/gruke-build/src/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;

public enum GradientDirection
{
    LeftToRight = 0,
    RightToLeft = 1,
}

#pragma warning disable CA1050
public static class ImageSharpExtensions
#pragma warning restore CA1050
{
    private static (PointF P1, PointF P2) Orientate(FontRectangle rect, GradientDirection dir) =>
        dir switch
        {
            GradientDirection.LeftToRight => (new PointF(rect.Left, rect.Top), new PointF(rect.Right, rect.Bottom)),
            GradientDirection.RightToLeft => (new PointF(rect.Right, rect.Bottom), new PointF(rect.Left, rect.Top)),
            _ => throw new ArgumentOutOfRangeException(nameof(dir))
        };

    public static LinearGradientBrush CreateLinearGradientBrush(
        this FontRectangle rect, 
        IEnumerable<ColorStop> gradientStages,
        GradientRepetitionMode gradientMode = GradientRepetitionMode.None,
        GradientDirection gradientDirection = GradientDirection.LeftToRight)
    {
        var (p1, p2) = Orientate(rect, gradientDirection);

        return new LinearGradientBrush(p1, p2,
            gradientMode,
            gradientStages as ColorStop[] ?? gradientStages.ToArray()
        );
    }

    extension(ColorStop)
    {
        public static IEnumerable<ColorStop> CreateEquidistant(params uint[] colors) 
            => ColorStop.CreateEquidistant(colors.Select(Color.FromRgbHex).ToArray());

        public static IEnumerable<ColorStop> CreateEquidistant(params Color[] colors)
        {
            foreach (var (index, color) in colors.Index())
            {
                var ratio = index == colors.Length - 1
                    ? 1f
                    : ((100f / colors.Length) * (index + 1)) / 100f;
                yield return new ColorStop(ratio, color);
            }
        }
    }

    extension(Color)
    {
        public static Color FromRgbHex(uint color)
        {
            var red = (byte)((color >> 16) & 255);
            var green = (byte)((color >> 8) & 255);
            var blue = (byte)(color & 255);

            return new Color(new Rgb24(red, green, blue));
        }
    }
}
