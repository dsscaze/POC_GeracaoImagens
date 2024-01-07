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
    public class ImageGeneratorHelper
    {
        public string ConvertTextToImage(string text, string fontname, int fontsize, Color bgcolor, Color fcolor, int Width, int Height)
        {

            //Bitmap bmp = new Bitmap(width, Height);
            //using (Graphics graphics = Graphics.FromImage(bmp))
            //{

            //    Font font = new Font(fontname, fontsize);
            //    graphics.FillRectangle(new SolidBrush(bgcolor), 0, 0, bmp.Width, bmp.Height);
            //    graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);
            //    graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //    graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            //    graphics.Flush();
            //    font.Dispose();
            //    graphics.Dispose();

            //}

            Font font = new Font(fontname, fontsize);

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font, Width);

            //set the stringformat flags to rtl
            StringFormat sf = new StringFormat();
            //uncomment the next line for right to left languages
            //sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            sf.Trimming = StringTrimming.Word;
            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap(Width, Height);

            drawing = Graphics.FromImage(img);
            //Adjust for high quality
            drawing.CompositingQuality = CompositingQuality.HighQuality;
            drawing.InterpolationMode = InterpolationMode.HighQualityBilinear;
            drawing.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawing.SmoothingMode = SmoothingMode.HighQuality;
            drawing.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            //paint the background
            drawing.Clear(bgcolor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(fcolor);

            drawing.DrawString(text, font, textBrush, new RectangleF(0, 0, Width, Height), sf);

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