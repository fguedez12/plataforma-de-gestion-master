using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GobEfi.Web.Data.Repositories
{
    public class CompraMedidorRepository : BaseRepository<CompraMedidor, long>, ICompraMedidorRepository
    {

        public CompraMedidorRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public async Task<bool> SaveAll()
        {
            return await ctx.SaveChangesAsync() > 0;
        }

        public void EliminarSegunCompraId(long compraId)
        {
            var compras = ctx.CompraMedidor.Where(cm => cm.CompraId == compraId).ToList();

            ctx.CompraMedidor.RemoveRange(compras);
        }
    }
}