using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Attributes
{
    public class SetReturnUrlAttribute : ActionFilterAttribute
    {
        public string Name { get; set; } = "ReturnUrl";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var returnUrl = filterContext.HttpContext.Request.Path.ToUriComponent();
            var controller = (filterContext.Controller as Controller);
            if (controller != null)
            {
                var url = $"{returnUrl}{controller.Request.QueryString.ToUriComponent()}";
                controller.HttpContext.Response.Cookies.Append(Name, url);
            }
        }
    }
}
