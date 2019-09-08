using GMSF.Model;
using Microsoft.AspNetCore.Mvc;

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