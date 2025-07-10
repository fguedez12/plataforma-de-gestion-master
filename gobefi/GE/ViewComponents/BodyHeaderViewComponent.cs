using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GobEfi.Web.ViewComponents
{
    public class BodyHeaderViewComponent : ViewComponent
    {

        public BodyHeaderViewComponent()
        { }

        public Task<IViewComponentResult> InvokeAsync(BodyHeader bodyHeader)
        {
            return Task.FromResult((IViewComponentResult)View("Default", bodyHeader));
        }
    }
}
