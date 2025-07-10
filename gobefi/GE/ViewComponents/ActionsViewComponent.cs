using GobEfi.Web.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.ViewComponents
{
    public class ActionsViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IActionList result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
