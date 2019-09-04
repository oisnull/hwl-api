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
    }
}