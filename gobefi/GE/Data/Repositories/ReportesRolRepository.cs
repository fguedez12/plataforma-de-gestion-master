using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GobEfi.Web.Data.Repositories
{
    public class ReportesRolRepository : BaseRepository<ReporteRol, long>, IReporteRolRepository
    {
        public ReportesRolRepository(ApplicationDbContext context, UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor) : base(context, userManager, httpContextAccessor)
        {
        }
    }
}