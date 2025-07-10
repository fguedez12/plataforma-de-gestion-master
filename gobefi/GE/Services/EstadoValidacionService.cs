using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EstadoValidacionModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class EstadoValidacionService : IEstadoValidacionService
    {
        private IEstadoValidacionRepository _repoEstadoValidacion;
        private IMapper _mapper;

        public EstadoValidacionService(
            IEstadoValidacionRepository estadoValidacionRepository,
            IMapper mapper
            )
        {
            _repoEstadoValidacion = estadoValidacionRepository;
            _mapper = mapper;
        }

        public IEnumerable<EstadoValidacionModel> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public EstadoValidacionModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EstadoValidacionModel>> GetAllAsync()
        {
            var result = _repoEstadoValidacion.All();
            return await _repoEstadoValidacion.All().ProjectTo<EstadoValidacionModel>(_mapper.ConfigurationProvider).OrderBy(a => a.Id).ToListAsync();

        }

        public string Insert(EstadoValidacionModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(EstadoValidacionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
