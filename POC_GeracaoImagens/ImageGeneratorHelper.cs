using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;

namespace POC_GeracaoImagens
{

    public class ImageWriter
    {
        public void WriteTextToImage(Bitmap image, string savePath, string textToWrite, FontFamily fontFamily)
        {
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                // Image is 444 x 248 pixels, this should place text center
                PointF writeLocation = new PointF(222f, 124f);
                var format = new StringFormat { Alignment = StringAlignment.Center };

                using (var font = new Font(fontFamily, 36, FontStyle.Regular))
                {
                    graphics.DrawString(textToWrite, font, Brushes.Red, writeLocation, format);
                }
                image.Save(savePath, ImageFormat.Jpeg);
            }
        }

        public Bitmap LoadImage(string imagePath)
        {
            Bitmap image = new Bitmap(imagePath);
            return image;
        }

        public FontFamily LoadFont(string fontPath, string fontName)
        {
            PrivateFontCollection fonts = new PrivateFontCollection();
            fonts.AddFontFile(fontPath);
            FontFamily font = fonts.Families[0];
            return font;
        }
    }

    public class ImageGeneratorHelper
    {
        public string ConvertTextToImage(string text, string fontname, int fontsize, FontStyle fontStyle, Color bgcolor, Color fcolor, int Width, int Height)
        {
            Font font = new Font(fontname, fontsize, fontStyle);

            //first, create a dummy bitmap just to get a graphics object
            Bitmap img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font, Width);

            //set the stringformat flags to rtl
            StringFormat sf = new StringFormat();
            //uncomment the next line for right to left languages
            //sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            sf.Trimming = StringTrimming.Word;
            sf.Alignment = StringAlignment.Center;

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap(Width, Height);
            //img.SetResolution(100, 100);

            drawing = Graphics.FromImage(img);
            //Adjust for high quality
            drawing.CompositingQuality = CompositingQuality.HighQuality;
            drawing.InterpolationMode = InterpolationMode.HighQualityBicubic;
            drawing.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawing.SmoothingMode = SmoothingMode.AntiAlias;
            drawing.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            //paint the background
            drawing.Clear(bgcolor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(fcolor);

            drawing.DrawString(text, font, textBrush, new RectangleF(0, Width * 0.40f, Width, Height), sf);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            string path = "~/images/";
            string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png";
            string fileFullName = HttpContext.Current.Server.MapPath(path) + fileName;

            img.Save(fileFullName);
            img.Dispose();

            return path + fileName;
        }
    }
}