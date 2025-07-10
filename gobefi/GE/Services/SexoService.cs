using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.SexoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class SexoService : ISexoService
    {
        private readonly ISexoRepository _repoSexo;
        private readonly IMapper _mapper;

        public SexoService(ISexoRepository repoSexo, IMapper mapper)
        {
            _repoSexo = repoSexo;
            _mapper = mapper;
        }

        public IEnumerable<SexoModel> All()
        {
            return _repoSexo.All().OrderBy(n => n.Nombre).ProjectTo<SexoModel>(_mapper.ConfigurationProvider).ToList();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public SexoModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(SexoModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(SexoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
