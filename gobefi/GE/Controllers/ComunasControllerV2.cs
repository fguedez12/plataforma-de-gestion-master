using AutoMapper;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/comunas/V2")]
    public class ComunasControllerV2 : CustomController
    {
        private readonly ApplicationDbContext context;

        public ComunasControllerV2(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            this.context = context;
        }

        [HttpGet("byRegionId/{id:long}")]
        public async Task<ActionResult<List<ComunaDTO>>> Get([FromRoute] long id)
        {
            var region = await context.Regiones.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) {
                return NotFound("La region seleccionada no existe");
            }
            var queryable = context.Comunas.Where(x => x.RegionId == id).AsQueryable();
            return await Get<Comuna, ComunaDTO>(queryable);
        }

    }
}
