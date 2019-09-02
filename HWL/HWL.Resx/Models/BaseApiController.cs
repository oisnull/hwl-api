using GMSF.Model;
using HWL.Service;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HWL.Resx.Models
{
    public class BaseApiController : ControllerBase
    {
        protected Tuple<bool, int> CheckToken(string token)
        {
            RequestValidate requestValidate = new RequestValidate();
            bool succ = requestValidate.CheckToken(token);
            int userid = requestValidate.GetCurrUserId();
            return new Tuple<bool, int>(succ, userid);
        }

        protected Request<T> GetDefaultRequest<T>(string token, T t)
        {
            return new Request<T>()
            {
                Head = new GMSF.HeadDefine.RequestHead()
                {
                    Token = token,
                },
                Body = t
            };
        }

        protected Response<ResxResult> GetResult(string resultCode, string message, ResxResult body = null)
        {
            Response<ResxResult> response = new Response<ResxResult>();
            response.Head = new GMSF.HeadDefine.ResponseHead()
            {
                ResultCode = resultCode,
                ResultMessage = message
            };
            response.Body = body;

            return response;
        }
    }
}