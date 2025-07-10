using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoAgrupamientoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class TipoAgrupamientoService : ITipoAgrupamientoService
    {
        private readonly ITipoAgrupamientoRepository _repoTipoAgrupamiento;
        private readonly IMapper _mapper;

        public TipoAgrupamientoService(ITipoAgrupamientoRepository repoTipoAgrupamiento, IMapper mapper)
        {
            _repoTipoAgrupamiento = repoTipoAgrupamiento;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TipoAgrupamientoModel>> GetAllAsync()
        {
            return await _repoTipoAgrupamiento.All().ProjectTo<TipoAgrupamientoModel>(_mapper.ConfigurationProvider).Where(a=>a.Active).OrderBy(a => a.Nombre).ToListAsync();
        }

        public IEnumerable<TipoAgrupamientoModel> All()
        {
            return _repoTipoAgrupamiento
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<TipoAgrupamientoModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public TipoAgrupamientoModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public string Insert(TipoAgrupamientoModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(TipoAgrupamientoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
