using GobEfi.FV.API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace GobEfi.FV.API.Filters
{
    public class MyActionFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.User;

            if (userContext == null || userContext.Claims.Count() == 0)
            {
                context.Result = new BadRequestObjectResult("Usuario no existe");
                return;
            }


        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }


    }
}
