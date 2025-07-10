using api_gestiona.DTOs.TipoDocumentos;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoDocumentosController : Controller
    {
        private readonly ITipoDocumentoService _tipoDocumentoService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TipoDocumentosController(ITipoDocumentoService tipoDocumentoService, ApplicationDbContext context, IMapper mapper)
        {
            _tipoDocumentoService = tipoDocumentoService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{etapa}")]
        public async Task<TipoDocumentoResponse> GetAll([FromRoute] int etapa)
        {
            var query = _context.TipoDocumentos.OrderBy(x => x.Orden).AsQueryable();

            if (etapa == 1) 
            {
                query = query.Where(x => x.Etapa1);
            }
            if (etapa == 2)
            {
                query = query.Where(x => x.Etapa2);
            }

            var tiposdocumentos = await query.ToListAsync();
            var list = _mapper.Map<List<TipoDocumentoDTO>>(tiposdocumentos);
            //var response = await _tipoDocumentoService.GetAll();   
            //return response;
            return new TipoDocumentoResponse() { Ok = true, TipoDocumentos = list };
        }
    }
}
