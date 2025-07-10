using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.EntornoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class EntornoService : IEntornoService
    {
        private readonly IEntornoRepository _repoEntorno;
        private readonly IMapper _mapper;

        public EntornoService(IEntornoRepository repoEntorno, IMapper mapper)
        {
            _repoEntorno = repoEntorno;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EntornoModel>> GetAllAsync()
        {
            return await _repoEntorno.All().ProjectTo<EntornoModel>(_mapper.ConfigurationProvider).Where(a=>a.Active).OrderBy(a => a.Nombre).ToListAsync();
        }

        public IEnumerable<EntornoModel> All()
        {
            return _repoEntorno
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<EntornoModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public EntornoModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public string Insert(EntornoModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(EntornoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
