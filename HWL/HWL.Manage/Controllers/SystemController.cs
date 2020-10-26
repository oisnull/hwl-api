using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HWL.Manage.Controllers
{
    public class SystemController : BaseController
    {
        public ActionResult DBBackup()
        {
            return View();
        }
    }
}