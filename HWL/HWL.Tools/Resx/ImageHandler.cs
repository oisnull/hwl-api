using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace HWL.Tools.Resx
{
    public class ImageHandler : ResxHandler
    {
        public bool IsThumbnail { get; set; }
        /// <summary>
        /// Default value is 1280
        /// </summary>
        public int ThumbnailImageWidth { get; set; } = 1280;
        /// <summary>
        /// Default value is 960
        /// </summary>
        public int ThumbnailImageHeight { get; set; } = 960;
        /// <summary>
        /// Default value is 50
        /// </summary>
        public int ThumbnailQuality { get; set; } = 50;
        public bool IsCoverOrigin { get; set; }

        private string ThumbnailImageName;
        private string SaveThumbnailLocalPath;
        private ResxImageResult ImageResult;

        public ImageHandler()
        {
        }

        private void BuildThumbnailPath()
        {
            if (this.IsCoverOrigin)
            {
                this.ThumbnailImageName = Path.GetFileName(base.SaveLocalPath);
                this.SaveThumbnailLocalPath = base.SaveLocalPath;
            }
            else
            {
                string ext = Path.GetExtension(base.SaveLocalPath);
                this.ThumbnailImageName = Path.GetFileName(base.SaveLocalPath).Replace(ext, "_s" + ext);
                this.SaveThumbnailLocalPath = string.Format("{0}/{1}", base.SaveLocalDirectory, this.ThumbnailImageName);
            }
        }

        public override ResxResult Upload(IFormFile file, bool useNewFileName = true)
        {
            ResxResult result = base.Upload(file, useNewFileName);
            if (!result.Success) return result;

            if (!this.IsThumbnail) return result;
            this.BuildThumbnailPath();

            var size = ImageSharpUtils.ThumbnailImage(base.SaveLocalPath, this.SaveThumbnailLocalPath, this.ThumbnailImageWidth, this.ThumbnailImageHeight, this.ThumbnailQuality);
            ImageResult = new ResxImageResult()
            {
                Success = result.Success,
                Message = result.Message,
                ResxAccessUrl = result.ResxAccessUrl,
                ImagePreviewUrl = string.Format("{0}/{1}", base.AccessUrl, this.ThumbnailImageName),
                ImageWidth = size.Item1,
                ImageHeight = size.Item2
            };

            return ImageResult;
        }

        public ResxImageResult GetUploadResult()
        {
            return this.ImageResult;
        }

    }
}
