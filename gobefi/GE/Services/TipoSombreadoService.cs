using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Models.TipoSombreadoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{

    public class TipoSombreadoService : ITipoSombreadoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TipoSombreadoService(ApplicationDbContext contex,
                                    IMapper mapper)
        {

            _mapper = mapper;
            _context = contex;
        }

        public async Task<List<TipoSombreadoModel>> Get() {

            var tiposSombreados = await _context.TipoSombreados.ToListAsync();
            return _mapper.Map<List<TipoSombreadoModel>>(tiposSombreados);
        }
    }
}
