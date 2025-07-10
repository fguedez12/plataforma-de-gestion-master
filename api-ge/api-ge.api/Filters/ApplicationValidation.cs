using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace api_gestiona.Filters
{
    public class ApplicationValidation : IActionFilter
    {
        private readonly IConfiguration _configuration;
        private string? HeaderKeyName;
        private string? HeaderKeyValue; 

        public ApplicationValidation(IConfiguration configuration)
        {
            _configuration = configuration;
            HeaderKeyName = _configuration["application-key:header"];
            HeaderKeyValue = _configuration["application-key:value"];
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue(HeaderKeyName, out StringValues headerValue);
            string? headerValueString = headerValue.FirstOrDefault();
            if (string.IsNullOrEmpty(headerValue))
            {
                context.Result = new UnauthorizedObjectResult("Unauthorized");
            }

            if (HeaderKeyValue != headerValueString)
            {
                context.Result = new UnauthorizedObjectResult("Unauthorized");
            }

        }

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
           
        //    var HeaderKeyName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("application-key")["header"];
        //    var HeaderKeyValue = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("application-key")["value"];

        //    if (context.HttpContext.Request.Headers[HeaderKeyName] == HeaderKeyValue)
        //    {
        //        context.Result = new UnauthorizedResult();
        //    }
            
        //}
    }
}
