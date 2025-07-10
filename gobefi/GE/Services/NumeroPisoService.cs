using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.NumeroPisoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class NumeroPisoService : INumeroPisoService
    {
        private readonly INumeroPisoRepository _repoNUmPiso;
        private readonly IMapper _mapper;

        public NumeroPisoService(INumeroPisoRepository repoNUmPiso, IMapper mapper)
        {
            _repoNUmPiso = repoNUmPiso;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NumeroPisoModel>> GetAllAsync()
        {
            return await _repoNUmPiso.All().ProjectTo<NumeroPisoModel>(_mapper.ConfigurationProvider).Where(a=>a.Active).OrderBy(a => a.Nombre).ToListAsync();
        }

        public IEnumerable<NumeroPisoModel> All()
        {
            return _repoNUmPiso
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<NumeroPisoModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public NumeroPisoModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public string Insert(NumeroPisoModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(NumeroPisoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
