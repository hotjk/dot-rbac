using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Utility.Captcha
{
    public class CaptchaImage
    {
        public CaptchaImage(int width, int height, int size,
            double warp,
            Color color,
            Color bgColor,
            string font)
        {
            this.Width = width;
            this.Height = height;
            this.WarpFactor = warp;
            xAmp = WarpFactor * width / 100;
            yAmp = WarpFactor * Height / 85;
            xFreq = 2 * Math.PI / width;
            yFreq = 2 * Math.PI / Height;
            Color = color;
            BgColor = bgColor;
            Font = font;
            Size = size;
            rect = new Rectangle(0, 0, Width, Height);
            rectText = Rectangle.Inflate(rect, (int)(Width / 10), (int)(height / 10));
        }

        public static CaptchaImage Mini()
        {
            return new CaptchaImage(90, 36, 28, 1.5, 
                Color.FromArgb(21, 72, 139), 
                Color.FromArgb(247, 247, 245),
                "Arial");
        }
        
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double WarpFactor { get; private set; }
        public double xAmp { get; private set; }
        public double yAmp { get; private set; }
        public double xFreq { get; private set; }
        public double yFreq { get; private set; }
        public Color Color { get; private set; }
        public Color BgColor { get; private set; }
        public String Font { get; private set; }
        public int Size { get; private set; }
        private Rectangle rect;
        private Rectangle rectText;

        public Bitmap Generate(string captchaText)
        {
            var bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                using (var solidBrush = new SolidBrush(BgColor))
                {
                    graphics.FillRectangle(solidBrush, rect);
                }
                using (var font = new Font(Font, Size))
                {
                    using (var fontFormat = new StringFormat(StringFormatFlags.NoWrap))
                    {
                        fontFormat.Alignment = StringAlignment.Center;
                        fontFormat.LineAlignment = StringAlignment.Center;

                        var path = new GraphicsPath();
                        path.AddString(captchaText, font.FontFamily, (int)font.Style, font.Size, rectText, fontFormat);
                        using (var solidBrush = new SolidBrush(Color))
                        {
                            graphics.FillPath(solidBrush, DeformPath(path));
                        }
                    }
                }
            }
            return bmp;
        }

        private GraphicsPath DeformPath(GraphicsPath graphicsPath)
        {
            var deformed = new PointF[graphicsPath.PathPoints.Length];
            var rng = new Random();
            var xSeed = rng.NextDouble() * 2 * Math.PI;
            var ySeed = rng.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < graphicsPath.PathPoints.Length; i++)
            {
                var original = graphicsPath.PathPoints[i];
                var val = xFreq * original.X * yFreq * original.Y;
                var xOffset = (int)(xAmp * Math.Sin(val + xSeed));
                var yOffset = (int)(yAmp * Math.Sin(val + ySeed));
                deformed[i] = new PointF(original.X + xOffset, original.Y + yOffset);
            }
            return new GraphicsPath(deformed, graphicsPath.PathTypes);
        }
    }
}
