using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GMSF.Model;
using HWL.Entity;
using HWL.Resx.Models;
using HWL.Service;
using HWL.Service.Resx.Body;
using HWL.ShareConfig;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HWL.Resx.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ResxController : BaseApiController
    {
        //private IHostingEnvironment hostingEnvironment;

        //public ResxController(IHostingEnvironment hostingEnvironment)
        //{
        //    this.hostingEnvironment = hostingEnvironment;
        //}

        [HttpPost]
        [Description("Resx upload handle")]
        public Response<ResxUploadResponseBody> ResxUpload([FromForm]List<IFormFile> files, int userId, string token)
        {
            Request<ResxUploadRequestBody> request = base.GetDefaultRequest(token, new ResxUploadRequestBody()
            {
                File = files.FirstOrDefault(),
                ResxType = ResxType.Other,
                UserId = userId
            });
            return ResxService.ResxUpload(request);
        }

        [HttpPost]
        [Description("Image upload handle")]
        public Response<ImageUploadResponseBody> ImageUpload([FromForm]List<IFormFile> files, int userId, string token, ResxType resxType = ResxType.Other)
        {
            Request<ImageUploadRequestBody> request = base.GetDefaultRequest(token, new ImageUploadRequestBody()
            {
                File = files.FirstOrDefault(),
                ResxType = resxType,
                UserId = userId
            });
            return ResxService.ImageUpload(request);
        }

        [HttpPost]
        [Description("Audio upload handle")]
        public Response<AudioUploadResponseBody> AudioUpload([FromForm]List<IFormFile> files, int userId, string token, ResxType resxType = ResxType.Other)
        {
            Request<AudioUploadRequestBody> request = base.GetDefaultRequest(token, new AudioUploadRequestBody()
            {
                File = files.FirstOrDefault(),
                ResxType = resxType,
                UserId = userId
            });
            return ResxService.AudioUpload(request);
        }

        [HttpPost]
        [Description("Video upload handle")]
        public Response<VideoUploadResponseBody> VideoUpload([FromForm]List<IFormFile> files, int userId, string token, ResxType resxType = ResxType.Other)
        {
            Request<VideoUploadRequestBody> request = base.GetDefaultRequest(token, new VideoUploadRequestBody()
            {
                File = files.FirstOrDefault(),
                ResxType = resxType,
                UserId = userId
            });
            return ResxService.VideoUpload(request);
        }

        //public Response<ResxResult> Video(string token = null)
        //{
        //    if (string.IsNullOrEmpty(token))
        //        token = "88888888";
        //    var ret = this.CheckToken(token);
        //    if (!ret.Item1)
        //    {
        //        return GetResult(GMSF.ResponseResult.FAILED, "TOKEN 验证失败");
        //    }
        //    //System.Threading.Thread.Sleep(1000);

        //    ResxModel resxModel = new ResxModel()
        //    {
        //        UserId = ret.Item2,
        //        ResxType = ResxType.ChatVideo,
        //        ResxSize = ResxConfigManager.VIDEO_MAX_SIZE,
        //        ResxTypes = ResxConfigManager.VIDEO_FILE_TYPES
        //    };
        //    try
        //    {
        //        VideoHandler2 resx = new VideoHandler2(
        //            HttpContext.Current.Request.Files,
        //            resxModel,
        //            CommonCs.GetObjToInt(HttpContext.Current.Request.Form["chunkindex"]),
        //            CommonCs.GetObjToInt(HttpContext.Current.Request.Form["chunkcount"]),
        //            HttpContext.Current.Request.Form["tempfileurl"]
        //            );
        //        //log.WriterLog("Video : chunkIndex="+ CommonCs.GetObjToInt(HttpContext.Current.Request.Form["chunkindex"]) + " chunkCount=" + CommonCs.GetObjToInt(HttpContext.Current.Request.Form["chunkcount"]) + " tempfileurl=" + HttpContext.Current.Request.Form["tempfileurl"]);

        //        var responseResult = resx.SaveStream();
        //        var res = GetResult(GMSF.ResponseResult.SUCCESS, null, responseResult);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.WriterLog(ex.Message);
        //        return GetResult(GMSF.ResponseResult.FAILED, ex.Message);
        //    }
        //}
    }
}
