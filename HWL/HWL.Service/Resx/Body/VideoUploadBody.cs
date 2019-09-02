using HWL.Entity;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Service.Resx.Body
{
    public class VideoUploadRequestBody
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }
        public ResxType ResxType { get; set; }
    }

    public class VideoUploadResponseBody
    {
        public ResxVideoResult ResxVideoResult { get; set; }
    }
}
