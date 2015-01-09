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
        public CaptchaImage(UInt32 width, UInt32 height, 
            bool warp = true, bool deform = false, bool noise = false)
        {
            this.Width = Math.Min((int)width, MAX_WIDTH);
            this.Height = Math.Min((int)height, MAX_HEIGHT); ;
            this.Warp = warp;
            this.Deform = deform;
            this.Noise = noise;
            Init();
        }

        private void Init()
        {
            BgColor = Color.FromArgb(255, 255, 255);

            _xAmp = WARP_FACTOR * Height / 100;
            _yAmp = WARP_FACTOR * Height / 85;
            _xAmp = Math.Max(Math.Min(_xAmp, 1), 2);
            _yAmp = Math.Max(Math.Min(_yAmp, 0.85), 2);
            _xFreq = 2 * Math.PI / Width;
            _yFreq = 2 * Math.PI / Height;
            _size = (int)(Height / 1);
            _rect = new Rectangle(0, 0, Width, Height);

            _errorBitmap = new Bitmap(Width, Height);
            using (var graphics = Graphics.FromImage(_errorBitmap))
            {
                graphics.DrawLine(Pens.Red, 0, 0, Width, Height);
                graphics.DrawLine(Pens.Red, 0, Height, Width, 0);
            }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Warp { get; private set; }
        public bool Deform { get; private set; }
        public bool Noise { get; private set; }
        public Color BgColor { get; set; }

        private const int MAX_WIDTH = 160;
        private const int MAX_HEIGHT = 80;
        private const double WARP_FACTOR = 2F;
        private const int MAX_COLOR = 180;
        private double _xAmp;
        private double _yAmp;
        private double _xFreq;
        private double _yFreq;
        private int _size;
        private Rectangle _rect;
        private Bitmap _errorBitmap;

        private readonly string[] _fonts = { "Arial", "Times New Roman", "Georgia", "Comic Sans MS" };

        public Bitmap Generate(string captchaText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(captchaText) || captchaText.Length > 10)
                {
                    throw new ArgumentException("Invalid captcha text.");
                }

                var bmp = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var solidBrush = new SolidBrush(BgColor))
                    {
                        graphics.FillRectangle(solidBrush, _rect);
                    }
                    using (var fontFormat = new StringFormat(StringFormatFlags.NoWrap))
                    {
                        fontFormat.Alignment = StringAlignment.Center;
                        fontFormat.LineAlignment = StringAlignment.Center;
                        int width = Width / captchaText.Length;
                        var random = new Random();
                        for (int i = 0; i < captchaText.Length; i++)
                        {
                            var path = new GraphicsPath();
                            var rect = new Rectangle(width * i, 0, width, Height);
                            using (var font = new Font(_fonts[random.Next(_fonts.Length - 1)], _size + random.Next(_size / 3)))
                            {
                                path.AddString(captchaText[i].ToString(), font.FontFamily, (int)font.Style, font.Size, rect, fontFormat);
                            }
                            if (this.Warp)
                            {
                                path = WarpPath(rect, path, random);
                            }
                            if (this.Deform)
                            {
                                path = DeformPath(path, random);
                            }
                            using (var solidBrush = new SolidBrush(RandomColor(random, MAX_COLOR)))
                            {
                                graphics.FillPath(solidBrush, path);
                            }
                            if (this.Noise)
                            {
                                AddNoise(graphics, random);
                            }
                        }
                    }
                }
                return bmp;
            }
            catch
            {
                return _errorBitmap;
            }
        }

        private static Color RandomColor(Random random, int max = 255)
        {
            return Color.FromArgb(random.Next(max),random.Next(max),random.Next(max));
        }

        private GraphicsPath DeformPath(GraphicsPath graphicsPath, Random random)
        {
            var deformed = new PointF[graphicsPath.PathPoints.Length];
            var xSeed = random.NextDouble() * 2 * Math.PI;
            var ySeed = random.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < graphicsPath.PathPoints.Length; i++)
            {
                var original = graphicsPath.PathPoints[i];
                var val = _xFreq * original.X * _yFreq * original.Y;
                var xOffset = (int)(_xAmp * Math.Sin(val + xSeed));
                var yOffset = (int)(_yAmp * Math.Sin(val + ySeed));
                deformed[i] = new PointF(original.X + xOffset, original.Y + yOffset);
            }
            return new GraphicsPath(deformed, graphicsPath.PathTypes);
        }

        private GraphicsPath WarpPath(Rectangle rect, GraphicsPath graphicsPath, Random random)
        {
            float v = rect.Height / 8.0F;
            PointF[] points =
            {
                new PointF(random.Next(rect.Width) / v, random.Next(
                    rect.Height) / v),
                new PointF(rect.Width - random.Next(rect.Width) / v, 
                    random.Next(rect.Height) / v),
                new PointF(random.Next(rect.Width) / v, 
                    rect.Height - random.Next(rect.Height) / v),
                new PointF(rect.Width - random.Next(rect.Width) / v,
                    rect.Height - random.Next(rect.Height) / v)
            };
            Matrix matrix = new Matrix();
            matrix.Translate(rect.X, rect.Y);
            graphicsPath.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
            return graphicsPath;
        }

        private void AddNoise(Graphics graphics, Random random)
        {
            int size = Math.Min(Height/10,4);
            for (int i = 0; i < (int)(Width * Height / 100F); i++)
            {
                int x = random.Next(Width);
                int y = random.Next(Height);
                int w = random.Next(size);
                int h = random.Next(size);
                using (var solidBrush = new SolidBrush(RandomColor(random, MAX_COLOR)))
                {
                    graphics.FillEllipse(solidBrush, x, y, w, h);
                }
            }
        }
    }
}
