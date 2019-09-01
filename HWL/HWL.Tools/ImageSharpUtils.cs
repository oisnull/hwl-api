using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Drawing;

namespace HWL.Tools
{
    public class ImageSharpUtils
    {
        public static Tuple<int, int> ThumbnailImage(string originImagePath, string newImagePath, int allocateWidth, int allocateHeight, int quality = 70)
        {
            using (Image<Rgba32> image = Image.Load(originImagePath))
            {
                var size = GetThumbnailSize(image, allocateWidth, allocateHeight);
                image.Mutate(x => x
                     .Resize(size.Item1, size.Item2)
                     );
                image.Save(newImagePath, new JpegEncoder()
                {
                    Quality = quality,
                    //Subsample = JpegSubsample.Ratio420
                });

                return size;
            }
        }

        public static Tuple<int, int> GetThumbnailSize(Image<Rgba32> image, int allocateWidth, int allocateHeight)
        {
            int sW = 0, sH = 0;
            Size tem_size = new Size(image.Width, image.Height);
            if (tem_size.Width > allocateHeight || tem_size.Width > allocateWidth)
            {
                if ((tem_size.Width * allocateHeight) > (tem_size.Width * allocateWidth))
                {
                    sW = allocateWidth;
                    sH = (allocateWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = allocateHeight;
                    sW = (tem_size.Width * allocateHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            return new Tuple<int, int>(sW, sH);
        }
    }
}
