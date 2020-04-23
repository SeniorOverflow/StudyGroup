using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace  StudyGroup.Script
{
    static class CropImage
    {
            public static Image Crop(this Image image, Rectangle selection)
                {
                    Bitmap bmp = new Bitmap(image);
           
                    if (bmp == null)
                        throw new ArgumentException("No valid bitmap");

                    Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

                    
                    image.Dispose();

                    return cropBmp;
                }
        
    }
}