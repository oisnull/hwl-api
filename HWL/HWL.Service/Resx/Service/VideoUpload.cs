using HWL.Entity;
using HWL.Service.Resx.Body;
using HWL.ShareConfig;
using HWL.Tools.Resx;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.Service.Resx.Service
{
    public class VideoUpload : GMSF.ServiceHandler<VideoUploadRequestBody, VideoUploadResponseBody>
    {
        public VideoUpload(VideoUploadRequestBody request) : base(request)
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

        public override VideoUploadResponseBody ExecuteCore()
        {
            string partialPath = CustomerEnumDesc.GetResxTypePath(request.ResxType, request.UserId);
            VideoHandler resx = new VideoHandler
            {
                ResxTypes = ResxConfigManager.VIDEO_FILE_TYPES,
                ResxSize = ResxConfigManager.VIDEO_MAX_SIZE,
                IsThumbnail = true,
                SaveLocalDirectory = string.Format("{0}{1}", AppConfigManager.UploadDirectory, partialPath),
                AccessUrl = string.Format("{0}{1}", ResxConfigManager.FileAccessUrl, partialPath)
            };
            ResxVideoResult result = resx.Upload(request.Files.FirstOrDefault());

            return new VideoUploadResponseBody()
            {
                ResxVideoResult = result
            };
        }
    }
}
