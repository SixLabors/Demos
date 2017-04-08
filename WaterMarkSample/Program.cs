using System;
using System.Numerics;
using ImageSharp;
using SixLabors.Fonts;

namespace WaterMarkSample
{
    class Program
    {
        static void Main(string[] args)
        {
            FontFamily family = FontCollection.SystemFonts.Find("Arial");

            Font font = new Font(family, 100f, FontStyle.Bold);

            ApplyWaterMarkSimple(font, "Copyright Person Name", "small.jpg", "small_simple.jpg");
            ApplyWaterMarkSimple(font, "Copyright Person Name", "large.jpg", "large_simple.jpg");

            ApplyWaterMarkScaled(font, "Copyright Person Name", "small.jpg", "small_scaled.jpg");
            ApplyWaterMarkScaled(font, "Copyright Person Name", "large.jpg", "large_scaled.jpg");
        }

        static void ApplyWaterMarkSimple(Font font, string text, string inputPath, string outputPath)
        {
            using (Image img = Image.Load(inputPath))
            {
                Color fill = new Color(128, 128, 128, 200);

                img.DrawText(text, font, fill, new Vector2(0, 0));

                img.Save(outputPath);
                
            }
        }

        static void ApplyWaterMarkScaled(Font font, string text, string inputPath, string outputPath)
        {
            using (Image img = Image.Load(inputPath))
            {
                TextMeasurer measurer = new TextMeasurer();

                // we can now get the dimensions of the bounding box of a piece of text
                SixLabors.Fonts.Size size = measurer.MeasureText(text, font, 72);

                //  calculate the scaling factor we need to change the fontsize by to fit the image
                float scalingFactor = Math.Min(img.Width / size.Width, img.Height / size.Height);

                Font scaledFont = new Font(font, scalingFactor * font.Size);
                Color fill = new Color(128, 128, 128, 200);

                img.DrawText(text, scaledFont
                    , fill, new Vector2(0, 0));

                img.Save(outputPath);
            }
        }
    }
}