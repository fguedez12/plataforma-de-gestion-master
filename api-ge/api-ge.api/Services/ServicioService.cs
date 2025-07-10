using api_gestiona.DTOs.Servicios;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class ServicioService : IServicioService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _manager;

        public ServicioService(IMapper mapper, ApplicationDbContext context, UserManager<Usuario> manager)
        {
            _mapper = mapper;
            _context = context;
            _manager = manager;
        }

        public async Task SaveJustificacion(ServicioDTO model)
        {
            var entity = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == model.Id);
            entity.Justificacion = model.Justificacion;
            await _context.SaveChangesAsync();
        }

        public async Task<ServicioResponse> GetByUserId(string userId)
        {
            var response = new ServicioResponse { Ok = true };
            response.Servicios = await GetServicesByUserId(userId);

            return response;
        }

        public async Task<List<ServicioDTO>> GetServicesByUserId(string userId) 
        {
            var user = await _manager.FindByIdAsync(userId) ?? throw new RequestFailedException("El usuario no existe");
            bool isAdmin = await _manager.IsInRoleAsync(user, "ADMINISTRADOR");

            var servicios = isAdmin ?
                await _context.Servicios.Where(x => x.Active).OrderBy(x => x.Nombre).ToListAsync() :
                await _context.UsuariosServicios.Where(x => x.UsuarioId == userId).Include(x => x.Servicio).Select(x => x.Servicio).Where(x => x.Active).OrderBy(x => x.Nombre).ToListAsync();

            var response = _mapper.Map<List<ServicioDTO>>(servicios);

            return response;
        }

        public async Task<List<ServicioListDTO>> GetAll()
        {
            var list = await _context.Servicios.Where(x=>x.Active).ToListAsync();
            var result = _mapper.Map<List<ServicioListDTO>>(list);
            return result;
        }

        public async Task<ServicioListDTO> GetById(long id)
        {
            var entity = await _context.Servicios.Where(x=>x.Id == id && x.Active).FirstOrDefaultAsync();
            var result = _mapper.Map<ServicioListDTO>(entity);
            return result;
        }

        public async Task<bool> Exist(long id)
        {
            var result = await _context.Servicios.AnyAsync(x=>x.Id == id && x.Active);
            return result;
        }

        public async Task<DiagnosticoDTO> GetDiagnostico(long id)
        {
            var service = await _context.Servicios
                .Where(x => x.Id == id).FirstOrDefaultAsync() ?? throw new Exception("NO existe el recurso");
            var result = _mapper.Map<DiagnosticoDTO>(service);
            return result;
        }
    }
}
