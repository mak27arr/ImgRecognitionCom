using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Text;
using TextRecognitionComObject.Interface;
using Tesseract;
using System.IO;

namespace TextRecognitionComObject.Classes
{
    public class ImageRecognitionT : IImageRecocgnition
    {
        public string ImgRecognition(string patch)
        {
            string tessPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");
            string result = "";

            using (var engine = new TesseractEngine(tessPath, "ukr", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(patch))
                {
                    var page = engine.Process(img);
                    result = page.GetText();
                    Console.WriteLine(result);
                }
            }
            return String.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;
        }

        public string ImgRecognition(byte[,] image)
        {
            throw new NotImplementedException();
        }

        public string ImgRecognition(Bitmap image)
        {
            throw new NotImplementedException();
        }


    }
}
