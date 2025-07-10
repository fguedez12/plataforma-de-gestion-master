using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.RegionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class RegionRepository : BaseRepository<Region, long>, IRegionRepository
    {
        public RegionRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public async Task<IEnumerable<RegionModel>> AllRegiones()
        {
            var result = (
                from r in ctx.Regiones
                orderby r.Posicion ascending
                select new RegionModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Posicion = r.Posicion
                }
            );

            return result;
        }

        public async Task<IEnumerable<RegionModel>> AllRegionesWithComunas()
        {
            var result = (
                from r in ctx.Regiones
                join c in ctx.Comunas on r.Id equals c.RegionId
                orderby r.Posicion ascending
                select new RegionModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Posicion = r.Posicion,
                    Comunas = r.Comunas
                }
            );
            return result;
        }
    }
}
