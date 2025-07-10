using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GobEfi.Web.Data.Repositories
{
    public class EnergeticoUnidadMedidaRepository : BaseRepository<EnergeticoUnidadMedida,long>, IEnergeticoUnidadMedidaRepository
    {

        public EnergeticoUnidadMedidaRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public List<EnergeticoUnidadMedida> GetByEnergeticoId(long enerId)
        {
            return ctx.EnergeticoUnidadesMedidas.Where(a => a.EnergeticoId == enerId).ToList();
        }

        public bool HasMoreOne(long energeticoId)
        {
            var items = ctx.EnergeticoUnidadesMedidas.Where(a => a.EnergeticoId == energeticoId).ToList();

            return items.Count > 1;
        }
    }
}
