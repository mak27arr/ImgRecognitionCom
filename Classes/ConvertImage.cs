using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace TextRecognitionComObject.Classes
{
    public class ConvertImage
    {
        public byte[] ToTiff(string patch)
        {
            return ToTiff(File.ReadAllBytes(patch));
        }
        private byte[] ToTiff(byte[] SourceImage)
        {
            //create a new byte array
            byte[] bin = new byte[0];

            //check if there is data
            if (SourceImage == null || SourceImage.Length == 0)
            {
                return bin;
            }

            //convert the byte array to a bitmap
            Bitmap NewImage;
            using (MemoryStream ms = new MemoryStream(SourceImage))
            {
                NewImage = new Bitmap(ms);
            }

            //set some properties
            Bitmap TempImage = new Bitmap(NewImage.Width, NewImage.Height);
            using (Graphics g = Graphics.FromImage(TempImage))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(NewImage, 0, 0, NewImage.Width, NewImage.Height);
            }
            NewImage = TempImage;

            //save the image to a stream
            using (MemoryStream ms = new MemoryStream())
            {
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 80L);

                NewImage.Save(ms, GetEncoderInfo("image/tiff"), encoderParameters);
                bin = ms.ToArray();
            }

            //cleanup
            NewImage.Dispose();
            TempImage.Dispose();

            //return data
            return bin;
        }
        //get the correct encoder info
        private ImageCodecInfo GetEncoderInfo(string MimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType.ToLower() == MimeType.ToLower())
                    return encoders[j];
            }
            return null;
        }
    }
}
