using HWL.Entity.Extends;
using HWL.Manage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace HWL.Manage
{
    public class BaseController : Controller
    {
        private const string ADMIN_SESSION_IDENTITY = "SystemAdmin";
        protected Admin currentAdmin { get; private set; }

        public BaseController()
        {
        }

        protected void LoadAdminSession()
        {
            string adminString = HttpContext.Session.GetString(ADMIN_SESSION_IDENTITY);
            if (!string.IsNullOrEmpty(adminString))
            {
                currentAdmin = JsonConvert.DeserializeObject<Admin>(adminString);
            }
        }

        protected void SetAdminSession(Admin admin)
        {
            if (admin == null) return;
            HttpContext.Session.SetString(ADMIN_SESSION_IDENTITY, JsonConvert.SerializeObject(admin));
        }

        protected void ClearAdminSession()
        {
            HttpContext.Session.Remove(ADMIN_SESSION_IDENTITY);
        }

        public virtual bool IsCheckAdmin()
        {
            return true;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.IsCheckAdmin())
            {
                this.LoadAdminSession();

                if (currentAdmin == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new ContentResult()
                        {
                            ContentType = "text/html",
                            Content = "<script>TimeoutError();</script>"
                        };
                    }
                    else
                    {
                        filterContext.Result = RedirectToRoute("Default", new { Controller = "Home", Action = "Login" });
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}