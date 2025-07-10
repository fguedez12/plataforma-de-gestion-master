using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EstructuraModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class EstructuraService : IEstructuraService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EstructuraService(ApplicationDbContext context,IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ICollection<EstructuraModel>> GetAll()
        {
            var listToDb = await _context.Estructuras.Include(x=>x.Materialidades).ThenInclude(x=>x.Materialidad)                    
                                                     .Include(x=>x.Aislaciones).ThenInclude(x=>x.Aislacion)
                                                     .Include(x => x.TiposSombreado).ThenInclude(x => x.TipoSombreado)
                                                     .Include(x=>x.PosicionVentanas).ThenInclude(x=>x.PosicionVentana)
                                                     .ToListAsync();


            foreach (var estructura in listToDb)
            {
                var listMat = new List<EstructuraMaterialidad>();
                foreach (var mat in estructura.Materialidades)
                {
                    if (mat.Materialidad.Active)
                    {
                        listMat.Add(mat);
                    }
                }
                estructura.Materialidades = listMat;

                var listAis = new List<EstructuraAislacion>();
                foreach (var ais in estructura.Aislaciones)
                {
                    if (ais.Aislacion.Active)
                    {
                        listAis.Add(ais);
                    }
                }
                estructura.Aislaciones = listAis;
            }




           return  _mapper.Map<ICollection<EstructuraModel>>(listToDb);

            
        }

        public async Task<List<EspesorModel>> GetEspesores()
        {
            var listDb = await _context.Espesores.ToListAsync();

            return _mapper.Map<List<EspesorModel>>(listDb);
        }

    }
}
