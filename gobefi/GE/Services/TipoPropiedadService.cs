using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoPropiedadModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class TipoPropiedadService : ITipoPropiedadService
    {
        private ITipoPropiedadRepository _repository;
        private ILogger _logger;
        private IMapper _mapper;

        public TipoPropiedadService(
            ITipoPropiedadRepository repository,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _logger = loggerFactory.CreateLogger<TipoPropiedadService>();
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<TipoPropiedadModel> All()
        {
            return _repository
                .All()
                .OrderBy(o => o.Orden)
                .ProjectTo<TipoPropiedadModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public TipoPropiedadModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(TipoPropiedadModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(TipoPropiedadModel model)
        {
            throw new NotImplementedException();
        }
    }
}
