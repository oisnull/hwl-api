using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GMSF.Model;
using HWL.Entity;
using HWL.Resx.Models;
using HWL.ShareConfig;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HWL.Resx.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ResxController : BaseApiController
    {
        private IHostingEnvironment hostingEnvironment;

        public ResxController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public Response<ResxResult> Image([FromForm]string token = null, [FromForm] ResxType resxType = ResxType.Other)
        {
            var ret = this.CheckToken(token);
            if (!ret.Item1)
            {
                return GetResult(GMSF.ResponseResult.FAILED, "TOKEN 验证失败");
            }

            string partialPath = string.Format("{0}{1}", AppConfigManager.UploadDirectory, CustomerEnumDesc.GetResxTypePath(resxType, ret.Item2));
            ImageHandler resx = new ImageHandler
            {
                ResxTypes = ResxConfigManager.IMAGE_FILE_TYPES,
                ResxSize = ResxConfigManager.IMAGE_MAX_SIZE,
                IsThumbnail = CustomerEnumDesc.IsTumbnail(resxType),
                SaveLocalDirectory = string.Format("{0}{1}", hostingEnvironment.WebRootPath, partialPath),
                AccessUrl = string.Format("{0}{1}", ResxConfigManager.FileAccessUrl, partialPath)
            };
            ResxResult result = resx.Upload(Request.Form.Files.FirstOrDefault());
            var res = GetResult(GMSF.ResponseResult.SUCCESS, null, result);
            //log.WriterLog(Newtonsoft.Json.JsonConvert.SerializeObject(res));
            return res;
        }

        public Response<ResxResult> Audio(string token = null)
        {
            var ret = this.CheckToken(token);
            if (!ret.Item1)
            {
                return GetResult(GMSF.ResponseResult.FAILED, "TOKEN 验证失败");
            }

            string partialPath = string.Format("{0}{1}", AppConfigManager.UploadDirectory, CustomerEnumDesc.GetResxTypePath(ResxType.ChatSound, ret.Item2));
            ResxHandler resx = new ResxHandler
            {
                ResxTypes = ResxConfigManager.SOUND_FILE_TYPES,
                ResxSize = ResxConfigManager.SOUND_MAX_SIZE,
                SaveLocalDirectory = string.Format("{0}{1}", hostingEnvironment.WebRootPath, partialPath),
                AccessUrl = string.Format("{0}{1}", ResxConfigManager.FileAccessUrl, partialPath)
            };
            ResxResult result = resx.Upload(Request.Form.Files.FirstOrDefault());
            var res = GetResult(GMSF.ResponseResult.SUCCESS, null, result);
            return res;
        }

        ///// <summary>
        ///// 分断接收处理
        ///// </summary>
        ///// <param name="token"></param>
        ///// <returns></returns>
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
