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
    public class PisoRepository : BaseRepository<Piso, long>, IPisoRepository
    {
        public PisoRepository(ApplicationDbContext context, UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor)
            : base(context, userManager, httpContextAccessor)
        {
        }

        public void Add<T>(T entity) where T : class
        {
            ctx.Add(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await ctx.SaveChangesAsync() > 0;

        }
    }
}
