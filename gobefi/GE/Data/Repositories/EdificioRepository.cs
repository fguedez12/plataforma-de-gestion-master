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
    public class EdificioRepository : BaseRepository<Edificio, long>, IEdificioRepository
    {
        public EdificioRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        {

        }

        public async Task<bool> SaveAll()
        {
            return await ctx.SaveChangesAsync() > 0;

        }
    }
}
