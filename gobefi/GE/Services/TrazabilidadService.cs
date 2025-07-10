using AutoMapper;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TrazabilidadModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class TrazabilidadService : ITrazabilidadService
    {
        private readonly IMapper _mapper;
        private readonly ITrazabilidadesRepository _repoTraza;
        private readonly UserManager<Usuario> _userManager;
        private readonly Usuario _currentUser;
        private readonly ILogger _logger;

        public TrazabilidadService(
            ILoggerFactory loggerFactory
            , IMapper mapper
            , ITrazabilidadesRepository repoTraza
            , UserManager<Usuario> userManager
            , IHttpContextAccessor httpContextAccessor)
        {
            _logger = loggerFactory.CreateLogger<CompraService>();
            _mapper = mapper;
            _repoTraza = repoTraza;
            _userManager = userManager;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        public async Task<long> Add<T>(T entity) where T : class
        {
            var trazaToSave = _mapper.Map<Trazabilidad>(entity);
            trazaToSave.CreatedAt = DateTime.Now;
            trazaToSave.CreatedBy = _currentUser.Id;
            trazaToSave.Active = true;
            trazaToSave.Version = 1;
            _repoTraza.Add(trazaToSave);

            await _repoTraza.SaveAll();

            _logger.LogInformation($"Trazabilidad [{trazaToSave.Id}] creada por el usuario [{_currentUser.Id}]");

            if (trazaToSave.Id < 0)
            {
                throw new Exception("Error al ingresar nueva trazabilidad, por favor revise los datos ingresados.");
            }


            return trazaToSave.Id;
        }

        public IEnumerable<TrazabilidadModel> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public TrazabilidadModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(TrazabilidadModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(TrazabilidadModel model)
        {
            throw new NotImplementedException();
        }
    }
}
