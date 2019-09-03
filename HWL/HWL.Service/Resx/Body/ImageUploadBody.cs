using HWL.Entity;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Service.Resx.Body
{
    public class ImageUploadRequestBody : ResxUploadRequestBody
    {
    }

    public class ImageUploadResponseBody
    {
        public ResxImageResult ResxImageResult { get; set; }
    }
}
