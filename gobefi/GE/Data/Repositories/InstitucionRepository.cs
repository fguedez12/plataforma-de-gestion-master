using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InstitucionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class InstitucionRepository : BaseRepository<Institucion, long>, IInstitucionRepository
    {
        private readonly ILogger _logger;

        public InstitucionRepository(
            ApplicationDbContext ctx,
            ILoggerFactory factory,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        {
            _logger = factory.CreateLogger<InstitucionRepository>();
        }

        public async Task<IEnumerable<InstitucionListModel>> AllByUser(string id)
        {
            // _logger.LogInformation("Usuario: {Nombre}", usuario.Nombres);
            bool v = await manager.IsInRoleAsync(usuario, Constants.Claims.ES_ADMINISTRADOR);
            // _logger.LogInformation("Usuario is Admin? {0}", (v ? "yes" : "no"));

            var result = v ? (
                from i in ctx.Instituciones
                join iu in ctx.UsuariosInstituciones
                    on new
                    {
                        A = i.Id,
                        B = id
                    }
                    equals new
                    {
                        A = iu.InstitucionId,
                        B = iu.UsuarioId
                    }
                into res
                from u in res.DefaultIfEmpty()
                select new InstitucionListModel
                {
                    Id = i.Id,
                    Nombre = i.Nombre,
                    Selected = u.UsuarioId == null ? false : true
                }
            ) : (
            from i in ctx.Instituciones
            join iu in ctx.UsuariosInstituciones
                on new
                {
                    A = i.Id,
                    B = id
                }
                equals new
                {
                    A = iu.InstitucionId,
                    B = iu.UsuarioId
                }
            into res
            from u in res
            select new InstitucionListModel
            {
                Id = i.Id,
                Nombre = i.Nombre,
                Selected = u.UsuarioId == null ? false : true
            }
            );
            return result;
        }
    }
}
