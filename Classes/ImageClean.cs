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
        public byte[] CleanLowThen(byte[] img,int r,int g,int b)
        {
            byte[] result;
            MemoryStream buf = new MemoryStream(img);
            Image image = Image.FromStream(buf, true);
            using (Graphics gra = Graphics.FromImage(image))
            {
                for(int ri = 0; ri < r; ri++)
                {
                    for (int gi = 0; gi < g; gi++)
                    {
                        for (int bi = 0; bi < b; bi++)
                        {
                            gra.Clear(Color.FromArgb(ri, gi, bi));
                        }
                    }
                }
            }
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                result = ms.ToArray();
            }
            return result;
        }

        public byte[] CleanHiThen(byte[] img, int r, int g, int b)
        {
            byte[] result;
            //MemoryStream buf = new MemoryStream(img);
            //Image image = Image.FromStream(buf, true);
            //using (Graphics gra = Graphics.FromImage(image))
            //{
            //for (int ri = r; ri < 255; ri++)
            //{
            //    for (int gi = g; gi < 255; gi++)
            //    {
            //        for (int bi = b; bi < 255; bi++)
            //        {
            //            gra.Clear(Color.FromArgb(ri, gi, bi));
            //        }
            //    }
            //}
            //}
            MemoryStream buf = new MemoryStream(img);
            Image image = Image.FromStream(buf, true);
            using (Graphics gra = Graphics.FromImage(image))
            {
                Parallel.For(r, 256, ri =>
                {
                    Parallel.For(g, 256, gi =>
                    {
                        for (int bi = b; bi < 255; bi++)
                        {
                            gra.Clear(Color.FromArgb(ri, gi, bi));
                        }
                    });
                });
            }

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                result = ms.ToArray();
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
