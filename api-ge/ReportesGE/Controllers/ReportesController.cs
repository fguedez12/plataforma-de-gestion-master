using Microsoft.AspNetCore.Mvc;

namespace ReportesGE.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ReportesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return Ok();
        }
    }
}
