using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace TextRecognitionComObject.Interface
{
    interface IImageRecocgnition
    {
        public string ImgRecognition(string patch);
        public string ImgRecognition(byte [,] image);
        public string ImgRecognition(Bitmap image);
    }
}
