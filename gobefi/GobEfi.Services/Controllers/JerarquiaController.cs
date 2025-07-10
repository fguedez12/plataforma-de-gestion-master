using GobEfi.Services.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class JerarquiaController : ControllerBase
    {
        private readonly DbContext context;

        public JerarquiaController(DbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = new JerarquiaModel();
            result.Instituciones = await context.Instituciones.ToListAsync();
            result.Servicios = await context.Servicios.ToListAsync();
            result.Edificios = await context.Edificios.ToListAsync();
            result.Divisiones = await context.Divisiones.ToListAsync();
            result.Regiones = await context.Regiones.ToListAsync();
            result.Provincias = await context.Provincias.ToListAsync();
            result.Comunas = await context.Comunas.ToListAsync();

            return Ok(result);
        }
    }
}
