using api_gestiona.DTOs.Edificios;
using api_gestiona.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdificiosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EdificiosController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("list-by-servicioId/{servicioId}")]
        public async Task<ActionResult> GetListByUnidadId([FromRoute] long servicioId)
        {
            var servicioFromDb = await _context.Servicios
                    .Include(x => x.Divisiones)
                    .ThenInclude(x => x.Edificio)
                    .ThenInclude(x => x.Comuna)
                    .ThenInclude(x => x.Region)
                    .FirstOrDefaultAsync(x => x.Id == servicioId);

            if (servicioFromDb == null)
            {
                return NotFound();
            }

            var listEdificios = new List<Edificio>();
            foreach (var division in servicioFromDb.Divisiones)
            {
                if (division.Edificio.Active)
                {
                    if (listEdificios.Where(x => x.Id == division.Edificio.Id).Count() == 0)
                    {
                        listEdificios.Add(division.Edificio);
                    }

                }
            }

            var edificioList = _mapper.Map<List<EdificioListDTO>>(listEdificios);
            return Ok(edificioList.OrderBy(x => x.ComunaId).ThenBy(x => x.Direccion));
        }

    }
}
