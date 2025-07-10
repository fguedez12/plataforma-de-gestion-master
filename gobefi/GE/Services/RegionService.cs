using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.RegionModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _repoRegion;
        private readonly IDivisionRepository _repoDivision;
        private readonly IComunaRepository _repoComuna;
        private readonly ILogger _logger;
        private IMapper _mapper;

        public RegionService(
            IRegionRepository repoRegion,
            IDivisionRepository repoDivision,
            IComunaRepository repoComuna,
            ILoggerFactory factory,
            IMapper mapper)
        {
            _repoRegion = repoRegion;
            _repoDivision = repoDivision;
            _repoComuna = repoComuna;
            _mapper = mapper;
            _logger = factory.CreateLogger<RegionService>();
        }

        public IEnumerable<RegionModel> All()
        {
            return _repoRegion.All().OrderBy(r => r.Nombre).ProjectTo<RegionModel>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<IEnumerable<RegionModel>> AllAsync()
        {

            var result = await _repoRegion.AllRegiones();
            return result.OrderBy(r => r.Nombre);
        }

        public async Task<IEnumerable<RegionModel>> AllRegionesWithComunas()
        {
            var result = await _repoRegion.AllRegionesWithComunas();
            return result;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public RegionModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RegionModel>> GetByServicioId(long servicioId)
        {
            var edificios = _repoDivision.Query().Where(d => d.ServicioId == servicioId).Include(d => d.Edificio).Select(d => d.Edificio);


            var comunasIds = edificios.Select(e => e.ComunaId).Distinct();

            var comunas = _repoComuna.Query().Where(c => comunasIds.Any(id => id == c.Id)).Distinct();

            //var revisa = comunas.ToList();

            var regiones = comunas.Include(c => c.Region).Select(c => c.Region).Distinct();
            
            //var re = regiones.ToList();

            return  regiones.OrderBy(r => r.Nombre).ProjectTo<RegionModel>(_mapper.ConfigurationProvider);
        }

        public long Insert(RegionModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(RegionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
