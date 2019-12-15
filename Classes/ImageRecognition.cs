using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using System.Text;
using TextRecognitionComObject.Interface;
using IronOcr;


namespace TextRecognitionComObject.Classes
{
    public class ImageRecognition : IImageRecocgnition
    {
        public string ImgRecognition(string patch)
        {
            var OCR = new AdvancedOcr()
            {
                CleanBackgroundNoise = true,
                EnhanceContrast = true,
                EnhanceResolution = true,
                Language = IronOcr.Languages.English.OcrLanguagePack,
                Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
                ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                DetectWhiteTextOnDarkBackgrounds = true,
                InputImageType = AdvancedOcr.InputTypes.AutoDetect,
                RotateAndStraighten = true,
                ReadBarCodes = true,
                ColorDepth = 4
            };
            var Results = OCR.Read(patch);
            return Results.Text;
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
