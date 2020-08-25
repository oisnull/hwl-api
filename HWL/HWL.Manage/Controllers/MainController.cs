using Microsoft.AspNetCore.Mvc;

namespace HWL.Manage.Controllers
{
    public class MainController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Default()
        {
            return View();
        }

    }
}