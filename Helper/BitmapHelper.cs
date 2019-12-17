using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TextRecognitionComObject.Helper
{
    public static class BitmapHelper
    {
        public static Bitmap Crop(this Bitmap image, Rectangle selection)
        {
            // Check if it is a bitmap:
            if (image == null)
                throw new ArgumentException("No valid bitmap");

            // Crop the image:
            Bitmap cropBmp = image.Clone(selection, image.PixelFormat);

            // Release the resources:
            image.Dispose();

            return cropBmp;
        }
    }
}
