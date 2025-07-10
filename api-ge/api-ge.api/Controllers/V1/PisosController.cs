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
    public class PisosController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _manager;

        public PisosController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
            _manager = manager;
        }

        [HttpPost("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {

            var userId = User.Claims.First(i => i.Type == "userId").Value;

            if (userId == null) return BadRequest();

            var pisoDb = await _context.Pisos.Include(x => x.Areas).FirstOrDefaultAsync(x => x.Id == id);
            if (pisoDb != null && userId != null)
            {
                pisoDb.Active = false;
                pisoDb.Version += pisoDb.Version;
                pisoDb.UpdatedAt = DateTime.Now;
                pisoDb.ModifiedBy = userId;

                foreach (var area in pisoDb.Areas)
                {
                    area.Active = false;
                    area.Version += area.Version;
                    area.UpdatedAt = DateTime.Now;
                    area.ModifiedBy = userId;
                }
            }

            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
