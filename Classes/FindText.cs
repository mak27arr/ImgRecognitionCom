using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TextRecognitionComObject.Classes
{
    [Guid("6AF6E007-6E60-4FAF-9ADA-F7BEFA58D457")]
    public class FindText
    {
        public string FindTExtInImg(string img_url)
        {
            string index = "clean";
            ConvertImage ci = new ConvertImage();
            var img = ci.ToTiff(img_url);
            ImageClean ic = new ImageClean();
            img = ic.CleanHiThen(img, 150, 150, 150, System.Drawing.Imaging.ImageFormat.Tiff);
            img = ic.KeepGray(img, 40, System.Drawing.Imaging.ImageFormat.Tiff);

            img_url += index;
            MemoryStream ms = new MemoryStream(img);
            Image i = Image.FromStream(ms);
            i.Save(img_url);
            ImageRecognitionT ir = new ImageRecognitionT();

            File.Delete(img_url);

            return ir.ImgRecognition(img_url);
        }
        public bool ConvertPDFToImg(string pdf_url, string img_url,int page)
        {
            if (File.Exists(pdf_url))
            {
                TiffImage myTiff = new TiffImage(pdf_url);
                if (myTiff.myImages.Count >= page)
                {
                    var img = myTiff.myImages[page] as Bitmap;
                    if (img != null)
                    {
                        img.Save(img_url, ImageFormat.Tiff);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
