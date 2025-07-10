using AutoMapper;
using GobEfi.ServiceN.Models.UserModels;
using GobEfi.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace GobEfi.ServiceN.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Acceso(AccesoInfo model)
        {
            if (model.TipoEntidad.ToUpper() == "INSTITUCION")
            {
                var result = await _context.UsuariosInstituciones.Where(us => us.UsuarioId == model.UsuarioId && us.InstitucionId == model.EntidadId).FirstOrDefaultAsync();
                return result == null ? false : true;
            }

            if (model.TipoEntidad.ToUpper() == "SERVICIO")
            {
                var result = await _context.UsuariosServicios.Where(us => us.UsuarioId == model.UsuarioId && us.ServicioId == model.EntidadId).FirstOrDefaultAsync();
                return result == null ? false : true;
            }

            if (model.TipoEntidad.ToUpper() == "DIVISION")
            {
                var result = await _context.UsuariosDivisiones.Where(us => us.UsuarioId == model.UsuarioId && us.DivisionId == model.EntidadId).FirstOrDefaultAsync();
                return result == null ? false : true;
            }

            return false;
        }

        public async Task<EntidadesModel> GetEntidades(string userId)
        {
            var userDb = await _context.Usuarios.Include(u => u.UsuarioRoles)
                .Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (userDb == null)
            {
                return null;
            }
            var institucionesDb = await _context.UsuariosInstituciones
                .Where(ui => ui.UsuarioId == userId)
                .Include(ui => ui.Institucion)
                .Select(ui => ui.Institucion).ToListAsync();
            var serviciosDb = await _context.UsuariosServicios
                .Where(ui => ui.UsuarioId == userId)
                .Include(ui => ui.Servicio)
                .Select(ui => ui.Servicio).ToListAsync();
            var divisionesDb = await _context.UsuariosDivisiones
                .Where(ui => ui.UsuarioId == userId)
                .Include(ui => ui.Division)
                .Select(ui => ui.Division).ToListAsync();

            var roleList = new List<RolesModel>();

            foreach (var role in userDb.UsuarioRoles)
            {
                var roleDb = await _context.Roles.Where(r => r.Id == role.RoleId).FirstOrDefaultAsync();

                roleList.Add(_mapper.Map<RolesModel>(roleDb));
            }

            var entidades = _mapper.Map<EntidadesModel>(userDb);

            entidades.Roles = roleList;
            entidades.Instituciones = _mapper.Map<List<InstitucionModel>>(institucionesDb);
            entidades.Servicios = _mapper.Map<List<ServicioModel>>(serviciosDb);
            entidades.Divisiones = _mapper.Map<List<DivisionModel>>(divisionesDb);

            return entidades;
        }
    }
}
