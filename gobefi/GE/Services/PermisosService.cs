using AutoMapper;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Models.PermisosModels;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class PermisosService : IPermisosService
    {
        private readonly IPermisosRepository repository;
        private readonly ILogger logger;
        private IMapper mapper;

        public IEnumerable<PermisosModel> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public PermisosModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(PermisosModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(PermisosModel model)
        {
            throw new NotImplementedException();
        }
    }
}
