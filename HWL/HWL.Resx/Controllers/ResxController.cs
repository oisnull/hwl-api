using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GMSF.Model;
using HWL.Resx.Models;
using Microsoft.AspNetCore.Mvc;

namespace HWL.Resx.Controllers
{
    [Route("resx/{action}")]
    [ApiController]
    public class ResxController : BaseApiController
    {
        // GET api/values
        //public Response<ResxResult> Image(string token = null, ResxType resxType = ResxType.Other)
        //{
        //    var ret = this.CheckToken(token);
        //    if (!ret.Item1)
        //    {
        //        return GetResult(GMSF.ResponseResult.FAILED, "TOKEN 验证失败");
        //    }

        //    UpfileHandler resx = new UpfileHandler(HttpContext.Current.Request.Files, new ResxModel()
        //    {
        //        UserId = ret.Item2,
        //        ResxType = resxType,
        //        //UserId = 1,
        //        //ResxType = ResxType.ChatImage,
        //        ResxSize = ResxConfigManager.IMAGE_MAX_SIZE,
        //        ResxTypes = ResxConfigManager.IMAGE_FILE_TYPES
        //    });

        //    try
        //    {
        //        var responseResult = resx.SaveStream();
        //        var res = GetResult(GMSF.ResponseResult.SUCCESS, null, responseResult);
        //        //log.WriterLog(Newtonsoft.Json.JsonConvert.SerializeObject(res));
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        //log.WriterLog(ex.Message);
        //        return GetResult(GMSF.ResponseResult.FAILED, ex.Message);
        //    }
        //}

        ////语音
        //public Response<ResxResult> Audio(string token = null)
        //{
        //    var ret = this.CheckToken(token);
        //    if (!ret.Item1)
        //    {
        //        return GetResult(GMSF.ResponseResult.FAILED, "TOKEN 验证失败");
        //    }

        //    UpfileHandler resx = new UpfileHandler(HttpContext.Current.Request.Files, new ResxModel()
        //    {
        //        UserId = ret.Item2,
        //        ResxType = ResxType.ChatSound,
        //        ResxSize = ResxConfigManager.SOUND_MAX_SIZE,
        //        ResxTypes = ResxConfigManager.SOUND_FILE_TYPES
        //    });

        //    try
        //    {
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
