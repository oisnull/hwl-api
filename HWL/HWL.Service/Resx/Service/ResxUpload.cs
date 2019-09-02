using HWL.Entity;
using HWL.Service.Resx.Body;
using HWL.ShareConfig;
using HWL.Tools.Resx;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HWL.Service.Resx.Service
{
    public class ResxUpload : GMSF.ServiceHandler<ResxUploadRequestBody, ResxUploadResponseBody>
    {
        public ResxUpload(ResxUploadRequestBody request) : base(request)
        {
        }

        protected override void ValidateRequestParams()
        {
            base.ValidateRequestParams();

            if (this.request.UserId <= 0)
                throw new ArgumentNullException("UserId");

            if (this.request.File == null)
                throw new ArgumentNullException("Files");
        }

        public override ResxUploadResponseBody ExecuteCore()
        {
            string partialPath = CustomerEnumDesc.GetResxTypePath(request.ResxType, request.UserId);
            ResxHandler resx = new ResxHandler
            {
                SaveLocalDirectory = string.Format("{0}{1}", AppConfigManager.UploadDirectory, partialPath),
                AccessUrl = string.Format("{0}{1}", ResxConfigManager.FileAccessUrl, partialPath)
            };
            ResxResult result = resx.Upload(request.File);
            return new ResxUploadResponseBody()
            {
                ResxResult = result
            };
        }
    }
}
