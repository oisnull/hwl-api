using HWL.Entity;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Service.Resx.Body
{
    public class AudioUploadRequestBody
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }
        public ResxType ResxType { get; set; }
    }

    public class AudioUploadResponseBody
    {
        public ResxResult ResxResult { get; set; }
    }
}
