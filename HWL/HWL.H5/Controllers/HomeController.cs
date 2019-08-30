using HWL.Entity.Models;
using HWL.H5.Models;
using HWL.Manage.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HWL.H5.Controllers
{
    public class HomeController : Controller
    {
        private HWLEntities dbContext;

        public HomeController(HWLEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShareApp()
        {
            ViewBag.AppUrl = new AppService(dbContext).GetAppLastVersionUrl();
            return View();
        }

        public ActionResult QRShare()
        {
            string url = "http://" + Request.Path.Value + "/home/shareapp";
            QRCodeBuild.CreateQR(url);
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}