using AutoMapper;
using AutoMapper.QueryableExtensions;
using GE.Models.ParametrosMedicionModels;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class ParametroMedicionService : IParametroMedicionService
    {
        private readonly IParametroMedicionRepository _repoParametroMedicion;
        private readonly IMapper _mapper;

        public ParametroMedicionService(IParametroMedicionRepository repoParametroMedicion, IMapper mapper)
        {
            _repoParametroMedicion = repoParametroMedicion;
            _mapper = mapper;
        }


        public IEnumerable<ParametrosMedicionModel> All()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ParametrosMedicionModel>> AllAsync()
        {
            var result = _repoParametroMedicion.Query().Include(u => u.UnidadesMedida);

            return await result.ProjectTo<ParametrosMedicionModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public ParametrosMedicionModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(ParametrosMedicionModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(ParametrosMedicionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
