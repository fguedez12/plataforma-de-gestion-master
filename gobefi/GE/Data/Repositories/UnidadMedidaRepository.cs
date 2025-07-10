using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class UnidadMedidaRepository : BaseRepository<UnidadMedida, long>, IUnidadMedidaRepository
    {
        public UnidadMedidaRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public List<UnidadMedida> Get(List<long> idsUnidadesMedida)
        {
            return ctx.UnidadesMedida.Where(a => idsUnidadesMedida.Contains(a.Id)).ToList();

        }
    }
}
