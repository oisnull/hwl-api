using HWL.Entity;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Service.Resx.Body
{
    public class VideoUploadRequestBody : ResxUploadRequestBody
    {
    }

    public class VideoUploadResponseBody
    {
        public ResxVideoResult ResxVideoResult { get; set; }
    }
}
