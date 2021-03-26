using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var userId = Session["id"] as String;
            var userName = Session["name"] as String;
            if (String.IsNullOrEmpty(userName)|| String.IsNullOrEmpty(userId))
            {
                //重定向至登录页面
                filterContext.Result = RedirectToAction("Index", "Login", new { Request.RawUrl });
                return;
            }

        }
    }
}