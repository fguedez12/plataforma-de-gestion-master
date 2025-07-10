using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Web.Services.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class DivisionRepository : BaseRepository<Division, long>, IDivisionRepository
    {
        private readonly ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public DivisionRepository(
            ApplicationDbContext ctx,
            ILoggerFactory factory,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        {
            _logger = factory.CreateLogger<DivisionRepository>();
        }

        public async Task<Division> GetDivision(long id)
        {
            var division = await ctx.Divisiones.FindAsync(id);

            division.NumerosClientes = await (from a in ctx.Divisiones
                                              join b in ctx.EnergeticoDivision on a.Id equals b.DivisionId
                                              join c in ctx.NumeroClientes on b.NumeroClienteId equals c.Id
                                              where a.Id == id && b.Active == true
                                              select c).ToListAsync();

            division.Medidores = await (from a in ctx.Divisiones
                                              join b in ctx.EnergeticoDivision on a.Id equals b.DivisionId
                                              join c in ctx.NumeroClientes on b.NumeroClienteId equals c.Id
                                              join d in ctx.Medidores on c.Id equals d.NumeroClienteId
                                              where a.Id == id && b.Active == true
                                              select d).ToListAsync();
            return division;
        }

        public async Task<IQueryable<Division>> Query(DivisionRequest request)
        {
            // _logger.LogInformation("Usuario: {Nombre}", usuario.Nombres);
            bool v = await manager.IsInRoleAsync(usuario, Constants.Claims.ES_ADMINISTRADOR);
            // _logger.LogInformation("Usuario es administrador? {0}", (v ? "yes" : "no"));

            var baseQuery = Query();
            baseQuery = baseQuery
                .Join(
                    ctx.UsuariosDivisiones,
                    division => division.Id,
                    usuarioDivision => usuarioDivision.DivisionId,
                    (division, usuarioDivision) => new { A = division, B = usuarioDivision }
                )
                .Where(a => a.B.UsuarioId == (!v ? usuario.Id : a.B.UsuarioId))
                .Select(a => a.A);

            return baseQuery;
        }

        public async Task<IEnumerable<DivisionListModel>> AllByUser(string id)
        {
            // _logger.LogInformation("Usuario: {Nombre}", usuario.Nombres);
            bool isAdmin = await manager.IsInRoleAsync(usuario, Constants.Claims.ES_ADMINISTRADOR);
            // _logger.LogInformation("Usuario is Admin? {0}", (isAdmin ? "yes" : "no"));

            var divisiones =
                isAdmin ? (
                from d in ctx.Divisiones
                join du in ctx.UsuariosDivisiones
                    on new
                    {
                        A = d.Id,
                        B = id
                    }
                    equals new
                    {
                        A = du.DivisionId,
                        B = du.UsuarioId
                    }
                into res
                from u in res.DefaultIfEmpty()
                select new DivisionListModel
                {
                    Id = d.Id,
                    Nombre = d.Nombre,
                    ReportaPMG = d.ReportaPMG,
                    Selected = (u.UsuarioId != null),
                    ServicioId = d.ServicioId,
                    InstitucionId = d.Servicio.InstitucionId
                }
            ) : (
            from d in ctx.Divisiones
            join du in ctx.UsuariosDivisiones
                on new
                {
                    A = d.Id,
                    B = id
                }
                equals new
                {
                    A = du.DivisionId,
                    B = du.UsuarioId
                }
            into res
            from u in res
            select new DivisionListModel
            {
                Id = d.Id,
                Nombre = d.Nombre,
                ReportaPMG = d.ReportaPMG,
                Selected = (u.UsuarioId != null),
                ServicioId = d.ServicioId,
                InstitucionId = d.Servicio.InstitucionId
            }
            );

            return await divisiones.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">A string.</param>
        /// <param name="institucionId">A long integer.</param>
        /// <returns></returns>
        /// See <see cref="IDivisionRepository.AllByUserAndInstitucion(string, long)"/>
        public async Task<IEnumerable<DivisionListModel>> AllByUserAndInstitucion(
            string userId, 
            long institucionId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DivisionListModel>> AllByUserAndServicio(string userId, long servicioId)
        {
            var result = await (
                from d in ctx.Divisiones
                join du in ctx.UsuariosDivisiones
                    on new
                    {
                        A = d.Id,
                        B = userId
                    }
                    equals new
                    {
                        A = du.DivisionId,
                        B = du.UsuarioId
                    }
                into res
                from u in res.DefaultIfEmpty()
                where d.ServicioId == servicioId
                select new DivisionListModel
                {
                    Id = d.Id,
                    Nombre = d.Nombre,
                    ReportaPMG = d.ReportaPMG,
                    Selected = (u.UsuarioId != null)
                }
            ).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<DivisionListModel>> GetByFilters(string userId, long servicioId, long regionId)
        {
            bool isAdmin = await manager.IsInRoleAsync(usuario, Constants.Claims.ES_ADMINISTRADOR);

            var result = await (
                from d in ctx.Divisiones
                join du in ctx.UsuariosDivisiones on new { a = d.Id, b = userId } equals new { a = du.DivisionId, b = du.UsuarioId }  into d_ud

                join s in ctx.Servicios on d.ServicioId equals s.Id
                join e in ctx.Edificios on d.EdificioId equals e.Id
                join c in ctx.Comunas on e.ComunaId equals c.Id
                join r in ctx.Regiones on c.RegionId equals r.Id
                //where du.UsuarioId == (isAdmin ? d_ud.DefaultIfEmpty().UsuarioId : userId)
                where ((servicioId == 0) || (s.Id == servicioId))
                && ((regionId == 0) || (r.Id == regionId))

                from res in d_ud.DefaultIfEmpty()
                select new DivisionListModel
                {
                    Id = d.Id,
                    Nombre = d.Nombre,
                    ReportaPMG = d.ReportaPMG,
                    Selected = (res.UsuarioId != null)
                }
            ).ToListAsync();

            return result;
        }
        public async Task SaveChangesAsync()
        {
            await ctx.SaveChangesAsync();
        }
    }
}

