using api_gestiona.DTOs.Viajes;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class AeropuertoService : IAeropuertoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AeropuertoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<AeropuertoListaDTO>> GetAeropuertosByPaisId(long paisId)
        {
            var list = await _context.Aeropuertos.Where(x=>x.PaisId == paisId).OrderBy(x=>x.Nombre).ToListAsync();
            var resp = _mapper.Map<List<AeropuertoListaDTO>>(list);
            return resp;
        }
    }
}
