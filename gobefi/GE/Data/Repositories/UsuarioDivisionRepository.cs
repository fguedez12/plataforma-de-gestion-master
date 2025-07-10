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
    public class UsuarioDivisionRepository : BaseRepository<UsuarioDivision, long>, IUsuarioDivisionRepository
    {
        public UsuarioDivisionRepository(ApplicationDbContext context, UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor) : base(context, userManager, httpContextAccessor)
        {
        }
    }
}
