using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Tools.Resx
{
    public class ResxResult
    {
        public ResxResult() { }

        public ResxResult(bool success, string message = null, string resxAccessUrl = null)
        {
            Success = success;
            Message = message;
            ResxAccessUrl = resxAccessUrl;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public string ResxAccessUrl { get; set; }
    }

    public class ResxImageResult : ResxResult
    {
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string ImagePreviewUrl { get; set; }
    }

    public class ResxVideoResult : ResxImageResult
    {
        public double Duration { get; set; }
    }
}
