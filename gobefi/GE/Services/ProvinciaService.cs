using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.ProvinciaModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly IProvinciaRepository _repoProvincia;
        private readonly IMapper _mapper;

        public ProvinciaService(IProvinciaRepository repoProvincia, IMapper mapper)
        {
            _repoProvincia = repoProvincia;
            _mapper = mapper;
        }

        public IEnumerable<ProvinciaModel> All()
        {
            return _repoProvincia.All().ProjectTo<ProvinciaModel>(_mapper.ConfigurationProvider).ToList();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public ProvinciaModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProvinciaModel>> GetByRegionId(long regionId)
        {
            var listaProvincias = await _repoProvincia.Query().Where(p => p.RegionId == regionId).ToListAsync();

            return _mapper.Map<IEnumerable<ProvinciaModel>>(listaProvincias);
        }

        public long Insert(ProvinciaModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(ProvinciaModel model)
        {
            throw new NotImplementedException();
        }
    }
}
