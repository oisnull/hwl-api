using HWL.Entity;
using HWL.Resx.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace HWL.Resx.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "没有权限访问";
        }
    }
}
