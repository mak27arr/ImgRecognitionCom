using Google.Cloud.Vision.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace TextRecognitionComObject.Classes
{
    public class ImageRecognitionGOCR
    {
        public ImageRecognitionGOCR(string patch)
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", patch);
        }
        public string ImgRecognition(string patch)
        {
            var client = ImageAnnotatorClient.Create();
            var image = Image.FromFile(patch);
            var response = client.DetectText(image);
            string full_text = "";
            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                {
                    full_text += annotation.Description;
                }
            }
            return full_text;
        }

        public string ImgRecognition(byte[,] image)
        {
            throw new NotImplementedException();
        }

        public string ImgRecognition(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }
    }
}
