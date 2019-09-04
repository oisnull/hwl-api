using HWL.Entity;
using HWL.Service.Resx.Body;
using HWL.ShareConfig;
using HWL.Tools.Resx;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.Service.Resx.Service
{
    public class ImageUpload : GMSF.ServiceHandler<ImageUploadRequestBody, ImageUploadResponseBody>
    {
        public ImageUpload(ImageUploadRequestBody request) : base(request)
        {
        }

        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();

            if (this.request.UserId <= 0)
                throw new ArgumentNullException("UserId");

            if (this.request.Files == null || this.request.Files.Count <= 0)
                throw new ArgumentNullException("Files");
        }

        public override ImageUploadResponseBody ExecuteCore()
        {
            string partialPath = CustomerEnumDesc.GetResxTypePath(request.ResxType, request.UserId);
            ImageHandler resx = new ImageHandler
            {
                ResxTypes = ResxConfigManager.IMAGE_FILE_TYPES,
                ResxSize = ResxConfigManager.IMAGE_MAX_SIZE,
                //IsThumbnail = CustomerEnumDesc.IsTumbnail(request.ResxType),
                IsThumbnail = false,
                SaveLocalDirectory = string.Format("{0}{1}", AppConfigManager.UploadDirectory, partialPath),
                AccessUrl = string.Format("{0}{1}", ResxConfigManager.FileAccessUrl, partialPath)
            };
            ResxImageResult result = resx.Upload(request.Files.FirstOrDefault());

            return new ImageUploadResponseBody()
            {
                ResxImageResult = result
            };
        }
    }
}
