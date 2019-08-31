using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Manage.Service;
using HWL.ShareConfig;
using HWL.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            string error;

            //apk文件上传处理
            //file_apk
            string folder = string.Format("{0}/{1}apkversion", hostingEnvironment.WebRootPath, AppConfigManager.UploadDirectory);
            var paths = UpfileHandler.Process(Request.Form.Files, folder, out error);
            if (paths != null && paths.Count > 0)
            {
                model.DownloadUrl = string.Format("{0}{1}{2}", AppConfigManager.FileAccessUrl, AppConfigManager.UploadDirectory, paths.FirstOrDefault());
            }
            else
            {
                return Json(new { state = -1, error = error });
            }

            int result = appService.AppVersionAction(model, out error);
            if (result > 0)
            {
                return Json(new { state = 1 });
            }
            else
            {
                return Json(new { state = -1, error = error });
            }
        }
    }
}