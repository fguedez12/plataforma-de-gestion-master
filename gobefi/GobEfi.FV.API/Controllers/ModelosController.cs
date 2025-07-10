using AutoMapper;
using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.API.Models.Entities;
using GobEfi.FV.Shared.DTOs;
using GobEfi.FV.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelosController :  CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ModelosController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}", Name = "obtenerModelo")]
        public async Task<ActionResult<ModeloDTO>> Get(int id)
        {
           
            return await Get<ModeloEm, ModeloDTO>(id);
        }

        [HttpGet("search")]
        public async Task<List<ModeloSearchDTO>> Search([FromQuery] string filter)
        {
            var queryable = _context.Modelos.AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                queryable = queryable.Where(x => x.Marca.Contains(filter) || x.Modelo.Contains(filter));
            }

            var listFromDb = await queryable.OrderBy(x=>x.Modelo).ToListAsync();

            return _mapper.Map<List<ModeloSearchDTO>>(listFromDb);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ModeloCreacionDTO modeloCreacionDTO)
        {
            var modeloFromDb = _context.Modelos.FirstOrDefault(m => m.IdEm == modeloCreacionDTO.Id);
            if (modeloFromDb == null)
            {
                var modeloToSave = _mapper.Map<ModeloEm>(modeloCreacionDTO);
                _context.Add(modeloToSave);
                await _context.SaveChangesAsync();
                var dtoLoectura = _mapper.Map<ModeloDTO>(modeloToSave);
                return new CreatedAtRouteResult("obtenerModelo", new { id = modeloToSave.Id }, dtoLoectura);
            }
            else
            {
                var modeloToSave = _mapper.Map<ModeloEm>(modeloCreacionDTO);
                modeloToSave.Id = modeloFromDb.Id;
                _context.Entry(modeloFromDb).State = EntityState.Detached;
                _context.Entry(modeloToSave).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var dtoLoectura = _mapper.Map<ModeloDTO>(modeloToSave);
                return new CreatedAtRouteResult("obtenerModelo", new { id = modeloToSave.Id }, dtoLoectura);
            }

           
        }
    }
}
