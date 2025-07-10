using AutoMapper;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoUnidadMedidaModels;
using GobEfi.Web.Models.UnidadMedidaModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class EnergeticoUnidadMedidaService : IEnergeticoUnidadMedidaService
    {
        public IEnergeticoUnidadMedidaRepository EnergeticoUnidadMedidaRepository;
        private readonly ILogger logger;
        private IMapper mapper;


        public EnergeticoUnidadMedidaService(IEnergeticoUnidadMedidaRepository EnergeticoUnidadMedidaRepository, 
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            this.EnergeticoUnidadMedidaRepository = EnergeticoUnidadMedidaRepository;
            this.logger = loggerFactory.CreateLogger<EnergeticoUnidadMedidaService>();
            this.mapper = mapper;


        }

        public IEnumerable<EnergeticoUnidadMedidaModel> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public EnergeticoUnidadMedidaModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(EnergeticoUnidadMedidaModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(EnergeticoUnidadMedidaModel model)
        {
            throw new NotImplementedException();
        }

        public bool hasMoreOne(long energeticoId)
        {
            bool exist = EnergeticoUnidadMedidaRepository.HasMoreOne(energeticoId);

            return exist;
        }

        public List<EnergeticoUnidadMedidaModel> getByEnergeticoId(long enerId)
        {
            List<EnergeticoUnidadMedida> unidadesMedidaFromRepository = EnergeticoUnidadMedidaRepository.GetByEnergeticoId(enerId);

            List<EnergeticoUnidadMedidaModel> toReturn = mapper.Map<List<EnergeticoUnidadMedidaModel>>(unidadesMedidaFromRepository);

            return toReturn;
        }
    }
}
