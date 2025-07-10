using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.InerciaTermicaModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class InerciaTermicaService : IInerciaTermicaService
    {
        private readonly IInerciaTermicaRepository _repoInercia;
        private readonly IMapper _mapper;

        public InerciaTermicaService(IInerciaTermicaRepository repoInercia, IMapper mapper)
        {
            _repoInercia = repoInercia;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InerciaTermicaModel>> GetAllAsync()
        {
            return await _repoInercia.All().ProjectTo<InerciaTermicaModel>(_mapper.ConfigurationProvider).Where(a=>a.Active).OrderBy(a => a.Nombre).ToListAsync();
        }

        public IEnumerable<InerciaTermicaModel> All()
        {
            return _repoInercia
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<InerciaTermicaModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public InerciaTermicaModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public string Insert(InerciaTermicaModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(InerciaTermicaModel model)
        {
            throw new NotImplementedException();
        }


    }
}
