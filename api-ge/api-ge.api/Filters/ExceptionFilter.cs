using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace api_gestiona.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception.Message, context.Exception);
        }
    }
}
