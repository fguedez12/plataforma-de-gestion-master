using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoTarifaModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class TipoTarifaService : ITipoTarifaService
    {

        private ITipoTarifaRepository tipoTarifaRepository;
        private readonly ILogger logger;
        private IMapper mapper;


        public TipoTarifaService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            ITipoTarifaRepository tipoTarifaRepository)
        {
            this.tipoTarifaRepository = tipoTarifaRepository;
            this.logger = loggerFactory.CreateLogger<TipoTarifaService>();
            this.mapper = mapper;
        }

        public IEnumerable<TipoTarifaModel> All()
        {
            return tipoTarifaRepository.All().ProjectTo<TipoTarifaModel>(mapper.ConfigurationProvider);
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public TipoTarifaModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(TipoTarifaModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(TipoTarifaModel model)
        {
            throw new NotImplementedException();
        }
    }
}
