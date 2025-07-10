using api_gestiona.DTOs;
using api_gestiona.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]

    public class TipoPropiedadController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _manager;

        public TipoPropiedadController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
            _manager = manager;
        }
        [HttpGet]
        public async Task<ActionResult<List<TipoPropiedadDTO>>> Get()
        {
            var list = await _context.TipoPropiedades.OrderBy(x => x.Orden).ToListAsync();
            return _mapper.Map<List<TipoPropiedadDTO>>(list);
        }

        [HttpGet("remove-vid")]
        public async Task<ActionResult> RemoveVid([FromQuery] int id)
        {
            try
            {
                var entity = await _context.Divisiones.ToListAsync();
                foreach (var item in entity)
                {
                    if (!string.IsNullOrEmpty(item.VehiculosIds))
                    {
                        var ids = item.VehiculosIds.Split(',');
                        foreach (var idv in ids)
                        {
                            if (idv == id.ToString())
                            {
                                ids = ids.Where(x => x != id.ToString()).ToArray();
                                item.VehiculosIds = string.Join(',', ids);
                                _context.Entry(item).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                        }
                    }

                }
                return NoContent();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
