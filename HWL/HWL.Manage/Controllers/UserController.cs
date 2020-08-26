using HWL.Entity;
using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Manage.Service;
using HWL.ShareConfig;
using HWL.Tools;
using HWL.Tools.Resx;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HWL.Manage.Controllers
{
    public class UserController : BaseController
    {
        private HWLEntities dbContext;
        private UserService userService;

        public UserController(HWLEntities dbContext)
        {
            this.dbContext = dbContext;
            this.userService = new UserService(dbContext);
        }

        // GET: User
        public ActionResult List(int page = 0)
        {
            int pageCount = 20;
            int recordCount = userService.GetUserCount();

            ViewBag.UseList = userService.GetUserList(page, pageCount);
            ViewBag.PageHtml = CommonCs.GetPageHtmlStr(recordCount, pageCount, page, 8, "/User/List", "");
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserEditInfo user)
        {
            try
            {
                string partialPath = CustomerEnumDesc.GetResxTypePath(ResxType.UserHeadImage);
                ImageHandler resx = new ImageHandler
                {
                    ResxTypes = ResxConfigManager.IMAGE_FILE_TYPES,
                    ResxSize = ResxConfigManager.IMAGE_MAX_SIZE,
                    IsThumbnail = false,
                    SaveLocalDirectory = string.Format("{0}{1}", AppConfigManager.UploadDirectory, partialPath),
                    AccessUrl = string.Format("{0}{1}", ResxConfigManager.FileAccessUrl, partialPath)
                };
                ResxImageResult result = resx.Upload(Request.Form.Files.FirstOrDefault());
                if (!result.Success)
                {
                    return Json(new { state = -1, error = result.Message });
                }
                else
                {
                    user.HeadImage = result.ResxAccessUrl;
                }

                int userId = userService.AddUser(user);
                if (userId <= 0)
                    return Json(new { state = -1, error = "Create user failed." });
            }
            catch (Exception ex)
            {
                return Json(new { state = -1, error = ex.Message });
            }
            return Json(new { state = 1 });
        }

        public ActionResult NewPos(string pos = null, DateTime? sd = null, DateTime? ed = null)
        {
            ViewBag.UserPos = userService.GetUserPosInfos(pos, sd, ed);
            return View(new Models.SearchPosModel() { Pos = pos, StartDate = sd?.ToString("yyyy-MM-dd"), EndDate = ed?.ToString("yyyy-MM-dd") });
        }

        public ActionResult Area(double? lon = null, double? lat = null)
        {
            ViewBag.GroupPos = userService.GetGroupAndUserInfos(lon, lat);
            //ViewBag.Lon = lon ?? 121.499328613281;
            //ViewBag.Lat = lat ?? 31.0719089508057;
            ViewBag.Lon = lon;
            ViewBag.Lat = lat;
            return View();
        }

        public ActionResult NearUsers(double? lon, double? lat)
        {
            ViewBag.NearUsers = userService.GetNearUserRadius(lon, lat);
            ViewBag.Lon = lon;
            ViewBag.Lat = lat;
            return View();
        }
    }
}