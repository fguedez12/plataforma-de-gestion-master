using System.Collections.Generic;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GobEfi.Web.Core.Contracts.Repositories;

namespace GobEfi.Web.Data.Repositories
{
    public class ReporteRepository : BaseRepository<Reporte, long>, IReporteRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReporteRepository(ApplicationDbContext context, UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor) : base(context, userManager, httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public object ObtenerData(long servicioId, bool isAdmin=false)
        {
            //var division = _context.Divisiones.Where(d => d.Id == divisionId).FirstOrDefault();

            var ret = (from s in _context.Servicios
                       //join us in _context.UsuariosServicios on s.Id equals us.ServicioId
                       //join u in _context.Users on us.UsuarioId equals u.Id
                       join i in _context.Instituciones on s.InstitucionId equals i.Id
                       join d in _context.Divisiones on s.Id equals d.ServicioId

                       join ud in _context.UsuariosDivisiones on d.Id equals ud.DivisionId
                       join u in _context.Users on ud.UsuarioId equals u.Id

                       join e in _context.Edificios on d.EdificioId equals e.Id
                       join c in _context.Comunas on e.ComunaId equals c.Id
                       join r in _context.Regiones on c.RegionId equals r.Id
                       join ur in _context.UserRoles on u.Id equals ur.UserId
                       join role in _context.Roles on ur.RoleId equals role.Id
                       where s.Id == servicioId
                       select new
                       {
                            InstitucionNombre = i.Nombre,
                            ServicioNombre = s.Nombre,
                            //EdificioNombre = e.Numero,
                            DivisionId = d.Id,
                            RegionNombre = r.Nombre,
                            ComunaNombre = c.Nombre,
                            DivisionSuperficie = string.IsNullOrEmpty(d.Superficie.ToString()) ? "" : d.Superficie.ToString(),
                            UsuarioNombre = (u.Nombres ?? "") + " " + (u.Apellidos ?? ""),
                            RolNombre = role.Nombre,
                            UsuarioEmail = u.Email
                        }).Distinct();

            return isAdmin ?ret.ToList() : ret.Where(r=>r.RolNombre!="AUDITOR").ToList() ;
        }
    }
}