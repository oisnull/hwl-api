using HWL.Entity.Models;
using HWL.H5.Models;
using HWL.Manage.Service;
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment hostingEnvironment;

        public HomeController(HWLEntities dbContext, IHostingEnvironment hostingEnvironment)
        {
            this.dbContext = dbContext;
            this.hostingEnvironment = hostingEnvironment;
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
            string url = string.Format("{0}://{1}/home/shareapp", Request.Scheme, Request.Host.Value);
            QRCodeBuild.CreateQR(url, hostingEnvironment.WebRootPath);
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}