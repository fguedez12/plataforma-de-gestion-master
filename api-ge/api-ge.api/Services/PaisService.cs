using api_gestiona.DTOs.Viajes;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class PaisService : IPaisService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PaisService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<PaisListaDTO>> GetPaisList()
        {
            var list = await _context.Paises.ToListAsync();
            var resp = _mapper.Map<List<PaisListaDTO>>(list);
            return resp;
        }
    }
}
