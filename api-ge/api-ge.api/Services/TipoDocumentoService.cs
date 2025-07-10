using api_gestiona.DTOs.TipoDocumentos;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoDocumentoService(ApplicationDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TipoDocumentoResponse> GetAll() 
        {
            try
            {
                var tipoDocumentos = await _context.TipoDocumentos.ToListAsync();
                var list = _mapper.Map<List<TipoDocumentoDTO>>(tipoDocumentos);
                var response = new TipoDocumentoResponse() { Ok = true, TipoDocumentos = list };
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
    }
}
