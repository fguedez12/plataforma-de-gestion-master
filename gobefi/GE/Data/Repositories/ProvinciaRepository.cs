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
    public class ProvinciaRepository : BaseRepository<Provincia, long>, IProvinciaRepository
    {
        public ProvinciaRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }
    }
}
