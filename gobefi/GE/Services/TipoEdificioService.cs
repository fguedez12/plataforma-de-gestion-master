using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoEdificioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class TipoEdificioService : ITipoEdificioService
    {
        private readonly ITipoEdificioRepository _repoTipoEdificio;
        private readonly IMapper _mapper;

        public TipoEdificioService(ITipoEdificioRepository repoTipoEdificio, IMapper mapper)
        {
            _repoTipoEdificio = repoTipoEdificio;
            _mapper = mapper;
        }

        public IEnumerable<TipoEdificioModel> All()
        {
            return _repoTipoEdificio
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<TipoEdificioModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public TipoEdificioModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public string Insert(TipoEdificioModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(TipoEdificioModel model)
        {
            throw new NotImplementedException();
        }
    }
}
