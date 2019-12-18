using System;
using System.Collections.Generic;
using System.Drawing;
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
    }
}
