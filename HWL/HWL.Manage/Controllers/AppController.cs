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
        public ActionResult Deploy(AppExt model)
        {
            string error;
            var file = Request.Form.Files.FirstOrDefault();
            if (file != null)
            {
                ResxHandler resx = new ResxHandler();
                resx.ResxTypes = new List<string>() { ".apk" };
                resx.SaveLocalDirectory = string.Format("{0}/apkversion", AppConfigManager.UploadDirectory);
                resx.AccessUrl = string.Format("{0}/apkversion", ResxConfigManager.FileAccessUrl);
                ResxResult result = resx.Upload(file);
                if (result.Success)
                {
                    model.Size = result.ResxSize;
                    model.DownloadUrl = result.ResxAccessUrl;
                }
                else
                {
                    error = result.Message;
                    return Json(new { state = -1, error = result.Message });
                }
            }

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

        public ActionResult PushVersion()
        {
            ViewBag.AppList = appService.GetAppVersionList(3);
            return View();
        }

        [HttpPost]
        public ActionResult PushToUsers(int appId, string userIds)
        {
            string error;
            int result = appService.AddAppVersionPush(appId, userIds, out error);
            if (result > 0)
            {
                return Json(new { state = 1 });
            }
            else
            {
                return Json(new { state = -1, error });
            }
        }
    }
}