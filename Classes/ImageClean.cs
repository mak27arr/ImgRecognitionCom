using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using TextRecognitionComObject.Interface;

namespace TextRecognitionComObject.Classes
{
    public class ImageClean : IImageClener
    {
        public byte[] CleanLowThen(byte[] img,int r,int g,int b)
        {
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
            return img;
        }
    }
}
