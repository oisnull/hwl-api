using System.ComponentModel;
using GMSF.Model;
using HWL.Resx.Models;
using HWL.Service;
using HWL.Service.Resx.Body;
using Microsoft.AspNetCore.Mvc;

namespace HWL.Resx.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ResxController : BaseApiController
    {
        [HttpPost]
        [Description("Resx upload handle")]
        public Response<ResxUploadResponseBody> ResxUpload([FromForm] ResxUploadRequestBody requestBody)
        {
            Request<ResxUploadRequestBody> request = base.GetDefaultRequest(requestBody.Token, requestBody);
            return ResxService.ResxUpload(request);
        }

        [HttpPost]
        [Description("Image upload handle")]
        public Response<ImageUploadResponseBody> ImageUpload([FromForm] ImageUploadRequestBody requestBody)
        {
            Request<ImageUploadRequestBody> request = base.GetDefaultRequest(requestBody.Token, requestBody);
            return ResxService.ImageUpload(request);
        }

        [HttpPost]
        [Description("Audio upload handle")]
        public Response<AudioUploadResponseBody> AudioUpload([FromForm] AudioUploadRequestBody requestBody)
        {
            Request<AudioUploadRequestBody> request = base.GetDefaultRequest(requestBody.Token, requestBody);
            return ResxService.AudioUpload(request);
        }

        [HttpPost]
        [Description("Video upload handle")]
        public Response<VideoUploadResponseBody> VideoUpload([FromForm] VideoUploadRequestBody requestBody)
        {
            Request<VideoUploadRequestBody> request = base.GetDefaultRequest(requestBody.Token, requestBody);
            return ResxService.VideoUpload(request);
        }
    }
}
