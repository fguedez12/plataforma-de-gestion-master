using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoUnidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class TipoUnidadService : ITipoUnidadService
    {
        private readonly ITipoUnidadRepository _repoTipoUnidad;
        private readonly IMapper _mapper;

        public TipoUnidadService(ITipoUnidadRepository repoTipoUnidad, IMapper mapper)
        {
            _repoTipoUnidad = repoTipoUnidad;
            _mapper = mapper;
        }

        public IEnumerable<TipoUnidadModel> All()
        {
            return _repoTipoUnidad
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<TipoUnidadModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public TipoUnidadModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(TipoUnidadModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(TipoUnidadModel model)
        {
            throw new NotImplementedException();
        }
    }
}
