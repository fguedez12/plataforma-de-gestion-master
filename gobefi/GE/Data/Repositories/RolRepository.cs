using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.RolModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class RolRepository : BaseRepository<Rol, string>, IRolRepository
    {
        public RolRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        { }

        public async Task<IEnumerable<RolListModel>> AllByUser(string id)
        {
            var result = (
                from r in ctx.Roles
                join ur in ctx.UserRoles
                    on new
                    {
                        A = r.Id,
                        B = id
                    }
                    equals new
                    {
                        A = ur.RoleId,
                        B = ur.UserId
                    }
                into res
                from u in res.DefaultIfEmpty()
                select new RolListModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Selected = u.UserId == null ? false : true
                }
            );

            return result;
        }

        public void UpdateRol(Rol entity)
        {
            entity.Name = "Admin";
            entity.NormalizedName = "Admin";
            ctx.Entry(entity);
            
            ctx.SaveChangesAsync();
        }
    }
}
