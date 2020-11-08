using HWL.Entity.Extends;
using HWL.Entity.Models;
using HWL.Manage.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HWL.Manage.Controllers
{
    public class HomeController : BaseController
    {
        private HWLEntities db;
        public HomeController(HWLEntities db)
        {
            this.db = db;
        }

        public override bool IsCheckAdmin()
        {
            return false;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginDo(string name, string pwd)
        {
            AdminService service = new AdminService(db);
            Admin result = service.Login(name, pwd);


            if (result.LoginStatus != AdminLoginStatus.Success)
            {
                return Json(new { state = -1, error = "用户名不存在或者密码错误" });
            }
            else
            {
                Models.AdminSessionManager.SetAdmin(result);
                return Json(new { state = 1, gto = "/Main/Default" });
            }
        }

        public ActionResult Logout()
        {
            Models.AdminSessionManager.ClearAdmin();
            return Redirect("/Home/Login");
        }
    }
}