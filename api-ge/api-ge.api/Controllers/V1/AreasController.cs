using api_gestiona.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AreasController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Usuario> _manager;

        public AreasController(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _manager = manager;
        }
        [HttpPost("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {

            var userId = User.Claims.First(i => i.Type == "userId").Value;

            if (userId == null) return BadRequest();

            var areaDb = await _context.Areas.FirstOrDefaultAsync(x => x.Id == id);
            if (areaDb != null && userId != null)
            {
                areaDb.Active = false;
                areaDb.Version += areaDb.Version;
                areaDb.UpdatedAt = DateTime.Now;
                areaDb.ModifiedBy = userId;
            }

            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
