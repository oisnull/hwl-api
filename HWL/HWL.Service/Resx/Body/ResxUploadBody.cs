using HWL.Entity;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Service.Resx.Body
{
    public class ResxUploadRequestBody
    {
        public string Token { get; set; }
        public List<IFormFile> Files { get; set; }
        public int UserId { get; set; }
        public ResxType ResxType { get; set; }
    }

    public class ResxUploadResponseBody
    {
        public ResxResult ResxResult { get; set; }
    }
}
