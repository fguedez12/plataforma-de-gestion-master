using api_gestiona.DTOs.Agregada;
using api_gestiona.DTOs.Agua;
using api_gestiona.DTOs.Pagination;
using api_gestiona.Entities;
using api_gestiona.Helpers;
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
    public class AgregadaController : CustomController
    {
        private readonly ApplicationDbContext _context;

        public AgregadaController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] long id)
        {
            var result = new AgregadaDTO();
            result.TotalColaboradoresServicio = await _context.Divisiones.Where(x => x.ServicioId == id && x.Active).SumAsync(x => x.Funcionarios);
            result.TotalOtrosColaboradores = await _context.Divisiones.Where(x => x.ServicioId == id && x.Active).SumAsync(x => x.NroOtrosColaboradores);
            result.TotalColaboradores = result.TotalColaboradoresServicio + result.TotalOtrosColaboradores;
            result.TotalUnidades = await _context.Divisiones.Where(x => x.ServicioId == id && x.Active).CountAsync();
            result.TotalUnidadesPmg = await _context.Divisiones.Where(x => x.ServicioId == id && x.Active && x.ReportaPMG).CountAsync();
            result.TotalUnidadesEe = await _context.Divisiones.Where(x => x.ServicioId == id && x.Active && x.IndicadorEE).CountAsync();
            result.TotalRestoItems = await _context.Divisiones.Where(x => x.ServicioId == id && x.Active && x.AnioInicioRestoItems == 2023).CountAsync();

            var servicio = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == id);
            result.ColaboradoresModAlcance = servicio!.ColaboradoresModAlcance;
            result.ModificacioAlcance = servicio.ModificacioAlcance;
            return Ok(result);
        }
    }
}
