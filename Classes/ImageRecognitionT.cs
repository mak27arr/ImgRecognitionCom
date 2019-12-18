using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Text;
using TextRecognitionComObject.Interface;
using Tesseract;
using System.IO;
using System.Runtime.InteropServices;

namespace TextRecognitionComObject.Classes
{
    [Guid("84496E60-23F3-4E07-9099-39BD2FA12323")]
    public class ImageRecognitionT : IImageRecocgnition
    {
        public string ImgRecognition(string patch)
        {
            string tessPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");
            string result = "";

            using (var engine = new TesseractEngine(tessPath, "eng+ukr", EngineMode.Default))
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
    }
}
