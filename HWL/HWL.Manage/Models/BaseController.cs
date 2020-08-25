using HWL.Entity.Extends;
using HWL.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace HWL.Manage
{
    public class BaseController : Controller
    {
        protected Admin CurrentAdmin { get; private set; }

        public BaseController()
        {
        }

        public virtual bool IsCheckAdmin()
        {
            return true;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.IsCheckAdmin())
            {
                this.CurrentAdmin = AdminSessionManager.GetAdmin();
                if (CurrentAdmin == null)
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