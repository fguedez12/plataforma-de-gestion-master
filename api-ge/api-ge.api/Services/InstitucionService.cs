using api_gestiona.DTOs.Instituciones;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class InstitucionService : IInstitucionService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InstitucionService(UserManager<Usuario> userManager,ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<InstitucionListDTO>> GetByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            bool isAdmin = await _userManager.IsInRoleAsync(user, "ADMINISTRADOR");
            if (isAdmin)
            {
                var listAll = await _context.Instituciones.Where(x => x.Active).OrderBy(o => o.Nombre).ToListAsync();

                var institucionesAll = _mapper.Map<List<InstitucionListDTO>>(listAll);

                return institucionesAll;
            }

            var query = _context.Instituciones.Where(x => x.Active).AsQueryable();

            query = query.Include(ur => ur.UsuariosInstituciones).Where(i => isAdmin ? true : i.UsuariosInstituciones.Any(ui => ui.UsuarioId == user.Id));
            query = query.Where(i => i.UsuariosInstituciones.Any(ui => ui.UsuarioId == userId));

            var list = await query.OrderBy(o => o.Nombre).ToListAsync();

            var instituciones = _mapper.Map<List<InstitucionListDTO>>(list);

            return instituciones;
        }
    }
}
