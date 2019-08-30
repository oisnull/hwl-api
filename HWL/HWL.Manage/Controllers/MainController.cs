using Microsoft.AspNetCore.Mvc;

namespace HWL.Manage.Controllers
{
    public class MainController : BaseController
    {
        public ActionResult Index()
        {
            return View(base.currentAdmin);
        }
       
        public ActionResult Default()
        {
            return View();
        }

    }
}