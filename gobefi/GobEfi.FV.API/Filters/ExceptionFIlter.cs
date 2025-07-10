using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Filters
{
    public class ExceptionFIlter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFIlter> _logger;

        public ExceptionFIlter(ILogger<ExceptionFIlter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);  
        }
    }
}
