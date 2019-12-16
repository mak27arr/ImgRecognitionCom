using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TextRecognitionComObject.Interface;
using System.Threading;
using System.Threading.Tasks;

namespace TextRecognitionComObject.Classes
{
    public class ImageClean : IImageClener
    {
        public byte[] CleanLowThen(byte[] img,int r,int g,int b, ImageFormat type)
        {
            byte[] result;
            MemoryStream buf = new MemoryStream(img);
            using (Bitmap bmp = new Bitmap(buf))
            {
                for (int i = 0; i <= bmp.Width - 1; i++)
                {
                    for (int j = 0; j < bmp.Height - 1; j++)
                    {
                        var pixel = bmp.GetPixel(i, j);
                        if (pixel.R < r && pixel.G < g && pixel.B < b)
                        {
                            bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                        }
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, type);
                    result = ms.ToArray();
                }
            }

            return result;
        }

        public byte[] CleanHiThen(byte[] img, int r, int g, int b, ImageFormat type)
        {
            byte[] result;
            MemoryStream buf = new MemoryStream(img);
            using (Bitmap bmp = new Bitmap(buf))
            {
                for (int i = 0; i <= bmp.Width - 1; i++)
                {
                    for (int j = 0; j < bmp.Height - 1; j++) { 
                        var pixel = bmp.GetPixel(i, j);
                        if (pixel.R>r&& pixel.G > g && pixel.B > b) {
                            bmp.SetPixel(i, j, Color.FromArgb(255,255,255,255));
                        }
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, type);
                    result = ms.ToArray();
                }
            }

            return result;
        }

        public byte[] KeepGray(byte[] img, int precent, ImageFormat type)
        {
            byte[] result;
            MemoryStream buf = new MemoryStream(img);
            using (Bitmap bmp = new Bitmap(buf))
            {
                for (int i = 0; i <= bmp.Width - 1; i++)
                {
                    for (int j = 0; j < bmp.Height - 1; j++)
                    {
                        var pixel = bmp.GetPixel(i, j);
                        if (pixel.G - precent > pixel.R || pixel.R > pixel.G + precent ||
                            pixel.B - precent > pixel.R || pixel.R > pixel.B + precent ||
                            pixel.R - precent > pixel.G || pixel.G > pixel.R + precent ||
                            pixel.B - precent > pixel.G || pixel.G > pixel.B + precent ||
                            pixel.G - precent > pixel.B || pixel.B > pixel.G + precent ||
                            pixel.R - precent > pixel.B || pixel.B > pixel.R + precent)
                        {
                            bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255, 255));
                        }
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, type);
                    result = ms.ToArray();
                }
            }

            return result;
        }

        public byte[] DetectImageRealSize(byte[] img,int retreat = 0)
        {
            byte[] result;
            MemoryStream buf = new MemoryStream(img);
            Image image = Image.FromStream(buf, true);
            using (Graphics gra = Graphics.FromImage(image))
            {
                //gra.
            }
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                result = ms.ToArray();
            }
            return result;
        }
    }
}
