using AutoMapper;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.RegionDTO;
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
    [Route("api/regiones/V2")]
    public class RegionesControllerV2 : CustomController
    {
        private readonly ApplicationDbContext context;

        public RegionesControllerV2(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            this.context = context;
        }

        public async Task<ActionResult<List<RegionDTO>>> Get()
        {
            var queryable = context.Regiones.OrderBy(x => x.Posicion).AsQueryable();
            return await Get<Region, RegionDTO>(queryable);
        }
    }

}
