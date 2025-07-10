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
    public class CompraRepository : BaseRepository<Compra, long>, ICompraRepository
    {

        public CompraRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public void Add<T>(T entity) where T : class
        {
            ctx.Add(entity);
        }

        public async Task<Compra> FindAsync(long id)
        {
            return await ctx.Compras.FindAsync(id);
        }

        public async Task<bool> SaveAll()
        {
            return await ctx.SaveChangesAsync() > 0;
            
        }


    }
}
