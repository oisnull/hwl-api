using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Drawing;
using System.IO;

namespace HWL.Tools
{
    public class ImageSharpUtils
    {
        public static Size ThumbnailImage(string originImagePath, string newImagePath, int allocateWidth, int allocateHeight, int quality = 70)
        {
            using (Image<Rgba32> image = Image.Load(originImagePath))
            {
                Size size = GetThumbnailSize(image, allocateWidth, allocateHeight);
                image.Mutate(x => x
                     .Resize(size.Width, size.Height)
                     );
                image.Save(newImagePath, new JpegEncoder()
                {
                    Quality = quality,
                    //Subsample = JpegSubsample.Ratio420
                });
                return size;
            }
        }

        public static Size GetImageSize(string originImagePath)
        {
            using (Image<Rgba32> image = Image.Load(originImagePath))
            {
                return new Size(image.Width, image.Height);
            }
        }

        public static Size GetThumbnailSize(Image<Rgba32> image, int allocateWidth, int allocateHeight)
        {
            int sW = 0, sH = 0;
            int sWidth = image.Width;
            int sHeight = image.Height;
            if (sHeight > allocateHeight || sWidth > allocateWidth)
            {
                if ((sWidth * allocateHeight) > (sHeight * allocateWidth))
                {
                    sW = allocateWidth;
                    sH = (allocateWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = allocateHeight;
                    sW = (sWidth * allocateHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }

            return new Size(sW, sH);
        }
    }
}
