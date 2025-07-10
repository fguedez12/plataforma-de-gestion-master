using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.UsuarioModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario, string>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _ctx;

        public UsuarioRepository(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor) : base(ctx, userManager, httpContextAccessor)
        {
            _ctx = ctx;
        }

        public Usuario GetWithRoles(string id)
        {
            var usuario = ctx.Usuarios
                .Where(u => u.Id == id)
                .First();
            ctx.Entry(usuario).Collection(ur => ur.UsuarioRoles).Load();
            return usuario;
        }

        public IEnumerable<UsuarioListExcelModel> ParaExcel(List<long> serviciosIds, bool isAdmin = false)
        {
            

            var items = (from u in _ctx.Usuarios
                         join ui in _ctx.UsuariosInstituciones on u.Id equals ui.UsuarioId
                         join i in _ctx.Instituciones on ui.InstitucionId equals i.Id
                         join us in _ctx.UsuariosServicios on u.Id equals us.UsuarioId
                         join s in _ctx.Servicios on us.ServicioId equals s.Id
                         join sex in _ctx.Sexo on u.SexoId equals sex.Id into tmp
                         join com in _ctx.Comunas on u.ComunaId equals com.Id
                         join reg in _ctx.Regiones on com.RegionId equals reg.Id
                         join ur in _ctx.UsuariosRoles on u.Id equals ur.UserId
                         join role in _ctx.Roles on ur.RoleId equals role.Id
                         from grup in tmp.DefaultIfEmpty()
                         where serviciosIds.Contains(us.ServicioId)
                         select new UsuarioListExcelModel
                        {
                            Institucion = i.Nombre,
                            Servicio = s.Nombre,
                            Nombres = u.Nombres,
                            Apellidos = u.Apellidos,
                            Email = u.Email,
                            Rut = u.Rut,
                            Sexo = string.IsNullOrEmpty(grup.Nombre) ? "" : grup.Nombre,
                            Region = reg.Nombre,
                            NumeroTelefono = string.IsNullOrEmpty(u.PhoneNumber) ? "" : u.PhoneNumber,
                            Certificado = u.Certificado ?? false ? "Si" : "No",
                            Validado = u.Validado ?? false ? "Si" : "No",
                            TipoGestor = role.Nombre,
                            FechaCreacion = u.CreatedAt,
                            FechaActualizacion = u.UpdatedAt
                        });
            


            return isAdmin?  items.ToList() : items.Where(i=>i.TipoGestor!="Auditor").ToList();
        }

        public async Task<ICollection<Servicio>> GetServiciosAsync()
        {
            return new HashSet<Servicio>();
        }

        public async Task<ICollection<Institucion>> GetInstitucionesAsync()
        {
            return new HashSet<Institucion>();
        }

    }
}
