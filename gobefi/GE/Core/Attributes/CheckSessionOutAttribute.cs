using GobEfi.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GobEfi.Web.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = filterContext.HttpContext;
            if (context.Session != null)
            {
                if (context.Session.IsAvailable)
                {
                    //var test = context.Session.Id;

                    if (context.Session.Keys.Count() == 0)
                    {
                        context.Session.Clear();
                        filterContext.Result = new RedirectResult("~/Account/Login");
                        return;
                    }
                        

                    //string sessionCookie = context.Request.Headers["Cookie"];

                    //if ((sessionCookie != null) && (sessionCookie.IndexOf("GE.Session") >= 0))
                    //{
                    //    //FormsAuthentication.SignOut();
                    //    string redirectTo = "~/Account/Login";
                    //    //if (!string.IsNullOrEmpty(context.Request.RawUrl))
                    //    //{
                    //        //redirectTo = string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
                    //        filterContext.Result = new RedirectResult(redirectTo);
                    //        return;
                    //    //}

                    //}
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
