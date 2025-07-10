using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ServicioModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class ServicioRepository : BaseRepository<Servicio, long>, IServicioRepository
    {
        private readonly ILogger _logger;

        public ServicioRepository(
            ApplicationDbContext ctx,
            ILoggerFactory factory,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        {
            _logger = factory.CreateLogger<ServicioRepository>();
        }

        //public async Task<IEnumerable<ServicioListModel>> AllByUserAndInstitucion(
        //    string userId, 
        //    long institucionId)
        //{
        //    var result = (
        //        from s in ctx.Servicios
        //        join su in ctx.UsuariosServicios
        //            on new
        //            {
        //                A = s.Id,
        //                B = userId
        //            }
        //            equals new
        //            {
        //                A = su.ServicioId,
        //                B = su.UsuarioId
        //            }
        //        into res
        //        from u in res.DefaultIfEmpty()
        //        where s.InstitucionId == institucionId
        //        select new ServicioListModel
        //        {
        //            Id = s.Id,
        //            Nombre = s.Nombre,
        //            Selected = u.UsuarioId == null ? false : true
        //        }
        //    );

        //    return result;
        //}

        public async Task<IEnumerable<ServicioListModel>> AllByUser(string id)
        {
            // _logger.LogInformation("Usuario: {Nombre}", usuario.Nombres);
            bool v = await manager.IsInRoleAsync(usuario, Constants.Claims.ES_ADMINISTRADOR);
            // _logger.LogInformation("Usuario is Admin? {0}", (v ? "yes" : "no"));

            var result = v ? (
                from s in ctx.Servicios
                join su in ctx.UsuariosServicios
                    on new
                    {
                        A = s.Id,
                        B = id
                    }
                    equals new
                    {
                        A = su.ServicioId,
                        B = su.UsuarioId
                    }
                into res
                from u in res.DefaultIfEmpty()
                select new ServicioListModel
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Selected = u.UsuarioId == null ? false : true
                }
            ) : (
            from s in ctx.Servicios
            join su in ctx.UsuariosServicios
                on new
                {
                    A = s.Id,
                    B = id
                }
                equals new
                {
                    A = su.ServicioId,
                    B = su.UsuarioId
                }
            into res
            from u in res
            select new ServicioListModel
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Selected = u.UsuarioId == null ? false : true
            }
            );

            return result;
        }
    }
}
