using HWL.Entity.Models;
using HWL.Manage.Service;
using HWL.Tools;
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
            int recordCount = 0;

            ViewBag.UseList = userService.GetUserList(page, pageCount);
            ViewBag.PageHtml = CommonCs.GetPageHtmlStr(recordCount, pageCount, page, 8, "/User/List/", "");
            return View();
        }
    }
}