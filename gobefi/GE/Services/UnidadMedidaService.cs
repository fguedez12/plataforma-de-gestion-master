using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.EnergeticoUnidadMedidaModels;
using GobEfi.Web.Models.UnidadMedidaModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly IUnidadMedidaRepository _repoUnidadMedida;
        private readonly ILogger _logger;
        private IMapper _mapper;


        public UnidadMedidaService(IUnidadMedidaRepository repoUnidadMedida,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _repoUnidadMedida = repoUnidadMedida;
            _logger = loggerFactory.CreateLogger<MedidorService>(); ;
            _mapper = mapper;
        }


        public IEnumerable<UnidadMedida> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public UnidadMedida Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<UnidadMedidaModel> Get(List<EnergeticoUnidadMedidaModel> unidadMedidaIds)
        {
            List<long> IdsUnidadesMedida = unidadMedidaIds.Select(a => a.UnidadMedidaId).ToList();

            List<UnidadMedidaModel>  toReturn = _mapper.Map<List<UnidadMedidaModel>>(_repoUnidadMedida.Get(IdsUnidadesMedida));

            return toReturn;
        }

        public async Task<IEnumerable<UnidadMedidaModel>> GetAll()
        {
            var result = _repoUnidadMedida.All().OrderBy(n => n.Nombre);

            return await result.ProjectTo<UnidadMedidaModel>(_mapper.ConfigurationProvider).ToListAsync();

        }

        public async Task<IEnumerable<UnidadMedidaModel>> GetAsociadosByEnergeticoId(long energeticoId)
        {
            var unidadesMedidas = _repoUnidadMedida.Query()
                .Include(um => um.EnergeticoUnidadMedidas)
                .Where(um => um.EnergeticoUnidadMedidas.Any(eum => eum.EnergeticoId == energeticoId))
                .OrderBy(n => n.Nombre);


            return await unidadesMedidas.ProjectTo<UnidadMedidaModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<EnergeticoForEditModel> GetForEdit(long? id)
        {
            var unidadMedida = await _repoUnidadMedida.Query().Where(um => um.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<EnergeticoForEditModel>(unidadMedida);
        }

        public async Task<IEnumerable<UnidadMedidaModel>> GetNoAsociadosByEnergeticoId(long energeticoId)
        {
            var unidadesMedidas = _repoUnidadMedida.Query()
                .Include(um => um.EnergeticoUnidadMedidas)
                .Where(um => !um.EnergeticoUnidadMedidas.Any(eum => eum.EnergeticoId == energeticoId))
                .OrderBy(n => n.Nombre);


            return await unidadesMedidas.ProjectTo<UnidadMedidaModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public long Insert(UnidadMedida model)
        {
            throw new NotImplementedException();
        }

        public void Update(UnidadMedida model)
        {
            throw new NotImplementedException();
        }
    }
}
