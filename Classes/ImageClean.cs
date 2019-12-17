using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TextRecognitionComObject.Interface;
using System.Threading;
using System.Threading.Tasks;
using TextRecognitionComObject.Stract;
using TextRecognitionComObject.Helper;
using System.Runtime.InteropServices;

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

        public byte[] TreamImg(byte[] img, int ofset, ImageFormat type)
        {
            byte[] result;
            
            MemoryStream buf = new MemoryStream(img);
            using (Bitmap bmp = new Bitmap(buf))
            {
                Coordinates lefttop = new Coordinates(0, 0);
                Coordinates leftdown = new Coordinates(0, 0);
                Coordinates righttop = new Coordinates(0, 0);
                Coordinates rightdown = new Coordinates(0, 0);
                //set max coordenates
                lefttop.SetMaxValue(bmp.Width, bmp.Height);
                leftdown.SetMaxValue(bmp.Width, bmp.Height);
                righttop.SetMaxValue(bmp.Width, bmp.Height);
                rightdown.SetMaxValue(bmp.Width, bmp.Height);
                //x
                for (int i = 0; i <= bmp.Width - 1; i++)
                {
                    //y
                    for (int j = 0; j < bmp.Height - 1; j++)
                    {
                        var pixel = bmp.GetPixel(i, j);
                        if (pixel.R != 255 || pixel.G !=255  || pixel.B != 255)
                        {
                            if((i<lefttop.X || lefttop.X == 0) &&(j < lefttop.Y || lefttop.Y == 0))
                            {
                                lefttop.X = i;
                                lefttop.Y = j;
                            }
                            if ((i < leftdown.X || leftdown.X == 0 || i == leftdown.X) && (j > leftdown.Y || leftdown.Y == 0))
                            {
                                leftdown.X = i;
                                leftdown.Y = j;
                            }
                            if ((i > righttop.X || righttop.X == 0) && (j < righttop.Y || righttop.Y == 0 || righttop.Y == j))
                            {
                                righttop.X = i;
                                righttop.Y = j;
                            }
                            if ((i > righttop.X || righttop.X == 0) && (j > righttop.Y || righttop.Y == 0 || righttop.Y == j))
                            {
                                righttop.X = i;
                                righttop.Y = j;
                            }
                        }
                    }
                }

                //leveling coordinates
                if (lefttop.Y > righttop.Y)
                {
                    lefttop.Y = righttop.Y;
                }
                else
                {
                    righttop.Y = lefttop.Y;
                }
                if (leftdown.Y > rightdown.Y)
                {
                    rightdown.Y = leftdown.Y;
                }
                else
                {
                    leftdown.Y = rightdown.Y;
                }
                if (lefttop.X > leftdown.X)
                {
                    lefttop.X = leftdown.X;
                }
                else
                {
                    leftdown.X = lefttop.X;
                }
                if (righttop.X > rightdown.X)
                {
                    rightdown.X = righttop.X;
                }
                else
                {
                    righttop.X = rightdown.X;
                }

                lefttop.X = lefttop.X - ofset;
                lefttop.Y = lefttop.Y - ofset;
                
                righttop.X = righttop.X + ofset;
                righttop.Y = righttop.Y - ofset;
                
                leftdown.X = leftdown.X - ofset;
                leftdown.Y = leftdown.Y - ofset;

                rightdown.X = rightdown.X + ofset;
                rightdown.Y = rightdown.Y + ofset;

                //thream images
                var tmp_bmp = bmp.Crop(new Rectangle(lefttop.X,lefttop.Y,(rightdown.X - lefttop.X), (rightdown.Y - lefttop.Y)));

                using (MemoryStream ms = new MemoryStream())
                {
                    tmp_bmp.Save(ms, type);
                    result = ms.ToArray();
                }
            }
            return result;
        }

        public byte[] DeleteNoise(byte[] img, double strength, ImageFormat type)
        {
            MemoryStream buf = new MemoryStream(img);
            using (var bitmap = new Bitmap(buf))
            {
                if (bitmap != null)
                {
                    var sharpenImage = bitmap.Clone() as Bitmap;

                    int width = bitmap.Width;
                    int height = bitmap.Height;

                    // Create sharpening filter.
                    const int filterWidth = 5;
                    const int filterHeight = 5;

                    var filter = new double[,]
                        {
                    {-1, -1, -1, -1, -1},
                    {-1,  2,  2,  2, -1},
                    {-1,  2, 16,  2, -1},
                    {-1,  2,  2,  2, -1},
                    {-1, -1, -1, -1, -1}
                        };

                    double bias = 1.0 - strength;
                    double factor = strength / 16.0;

                    var result = new Color[bitmap.Width, bitmap.Height];

                    // Lock image bits for read/write.
                    if (sharpenImage != null)
                    {
                        BitmapData pbits = sharpenImage.LockBits(new Rectangle(0, 0, width, height),
                                                                    ImageLockMode.ReadWrite,
                                                                    PixelFormat.Format24bppRgb);

                        // Declare an array to hold the bytes of the bitmap.
                        int bytes = pbits.Stride * height;
                        var rgbValues = new byte[bytes];

                        // Copy the RGB values into the array.
                        Marshal.Copy(pbits.Scan0, rgbValues, 0, bytes);

                        int rgb;
                        // Fill the color array with the new sharpened color values.
                        ///for (int x = 0; x < width; ++x)
                        //{
                        Parallel.For(0, width - 1, x =>{ 
                        for (int y = 0; y < height; ++y)
                        {
                            double red = 0.0, green = 0.0, blue = 0.0;

                            for (int filterX = 0; filterX < filterWidth; filterX++)
                            {
                                for (int filterY = 0; filterY < filterHeight; filterY++)
                                {
                                    int imageX = (x - filterWidth / 2 + filterX + width) % width;
                                    int imageY = (y - filterHeight / 2 + filterY + height) % height;

                                    rgb = imageY * pbits.Stride + 3 * imageX;

                                    red += rgbValues[rgb + 2] * filter[filterX, filterY];
                                    green += rgbValues[rgb + 1] * filter[filterX, filterY];
                                    blue += rgbValues[rgb + 0] * filter[filterX, filterY];
                                }

                                rgb = y * pbits.Stride + 3 * x;

                                int r = Math.Min(Math.Max((int)(factor * red + (bias * rgbValues[rgb + 2])), 0), 255);
                                int g = Math.Min(Math.Max((int)(factor * green + (bias * rgbValues[rgb + 1])), 0), 255);
                                int b = Math.Min(Math.Max((int)(factor * blue + (bias * rgbValues[rgb + 0])), 0), 255);

                                result[x, y] = Color.FromArgb(r, g, b);
                            }
                        }
                    });
                        //}

                        // Update the image with the sharpened pixels.
                        for (int x = 0; x < width; ++x)
                        {
                            for (int y = 0; y < height; ++y)
                            {
                                rgb = y * pbits.Stride + 3 * x;

                                rgbValues[rgb + 2] = result[x, y].R;
                                rgbValues[rgb + 1] = result[x, y].G;
                                rgbValues[rgb + 0] = result[x, y].B;
                            }
                        }

                        // Copy the RGB values back to the bitmap.
                        Marshal.Copy(rgbValues, 0, pbits.Scan0, bytes);
                        // Release image bits.
                        sharpenImage.UnlockBits(pbits);
                    }
                    byte[] res;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        sharpenImage.Save(ms, type);
                        res = ms.ToArray();
                    }
                    return res;
                }
            }
            return null;
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
