using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Manage.Service;
using HWL.ShareConfig;
using HWL.Tools;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HWL.Manage.Controllers
{
    public class AppController : BaseController
    {
        private HWLEntities dbContext;
        private AppService appService;
        private IHostingEnvironment hostingEnvironment;

        public AppController(HWLEntities dbContext, IHostingEnvironment hostingEnvironment)
        {
            this.dbContext = dbContext;
            this.hostingEnvironment = hostingEnvironment;
            this.appService = new AppService(dbContext);
        }


        public ActionResult List()
        {
            ViewBag.AppList = appService.GetAppVersionList();
            return View();
        }
        public ActionResult Publish(int? id)
        {
            AppExt model = appService.GetAppVersionInfo(id ?? 0);
            if (model == null)
            {
                model = new AppExt();
                model.PublishTime = DateTime.Now;
            }

            return View(model);
        }
        public ActionResult DelAppVersion(int? id)
        {
            string error;
            int result = appService.DeleteAppVersion(id ?? 0, out error);
            if (result > 0)
            {
                return Json(new { state = 1 });
            }
            else
            {
                return Json(new { state = -1, error = error });
            }
        }

        [HttpPost]
        public ActionResult Action(AppExt model)
        {
            string partialPath = string.Format("{0}apkversion", AppConfigManager.UploadDirectory);

            ResxHandler resx = new ResxHandler();
            resx.ResxTypes = new List<string>() { ".apk" };
            resx.SaveLocalDirectory = string.Format("{0}{1}", hostingEnvironment.WebRootPath, partialPath);
            resx.AccessUrl = string.Format("{0}{1}", AppConfigManager.FileAccessUrl, partialPath);
            ResxResult result = resx.Upload(Request.Form.Files.FirstOrDefault());

            if (result.Success)
            {
                string error;
                model.DownloadUrl = result.ResxAccessUrl;
                int ret = appService.AppVersionAction(model, out error);
                if (ret > 0)
                {
                    return Json(new { state = 1 });
                }
                else
                {
                    return Json(new { state = -1, error });
                }
            }
            else
            {
                return Json(new { state = -1, error = result.Message });
            }
        }
    }
}