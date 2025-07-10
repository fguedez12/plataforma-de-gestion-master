using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GobEfi.Web.Data.Repositories
{
    public class ArchivoAdjuntoRepository : BaseRepository<ArchivoAdjunto, long>, IArchivoAdjuntoRepository
    {

        public ArchivoAdjuntoRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public void Add<T>(T entity) where T : class
        {
             ctx.Add(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}