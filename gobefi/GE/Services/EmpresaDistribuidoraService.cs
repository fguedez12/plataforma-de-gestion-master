using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EmpresaDistribuidoraModels;
using GobEfi.Web.Services.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GobEfi.Web.Services
{
    public class EmpresaDistribuidoraService : IEmpresaDistribuidoraService
    {
        private readonly IEmpresaDistribuidoraRepository _repoEmpresaDistribuidora;
        private readonly IComunaRepository _repoComuna;
        private readonly ILogger _logger;
        private IMapper _mapper;


        public EmpresaDistribuidoraService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IEmpresaDistribuidoraRepository repoEmpresaDistribuidora,
            IComunaRepository repoComuna)
        {
            _repoEmpresaDistribuidora = repoEmpresaDistribuidora;
            _repoComuna = repoComuna;
            _logger = loggerFactory.CreateLogger<EnergeticoDivisionService>();
           _mapper = mapper;
        }

        private IQueryable<EmpresaDistribuidora> Query(EmpresaDistribuidoraRequest request)
        {
            var query = _repoEmpresaDistribuidora.Query();

            if (!string.IsNullOrEmpty(request.Nombre))
            {
                query = query.Where(u => EF.Functions.Like(u.Nombre, $"%{request.Nombre}%"));
            }

            if (request.EnergeticoId > 0)
            {
                query = query.Where(e => e.EnergeticoId == request.EnergeticoId);
            }

            if (request.RegionId > 0)
            {
                var comunasPorRegion = _repoComuna.Query().Where(c => c.RegionId == request.RegionId);


                query = query.Include(e => e.EmpresaDistribuidoraComunas);
                query = query.Where(e => e.EmpresaDistribuidoraComunas.Any(edc => comunasPorRegion.Any(cr => cr.Id == edc.ComunaId) ));
            }

            if (request.ProvinciaId > 0)
            {
                var comunasPorRegion = _repoComuna.Query().Where(c => c.ProvinciaId == request.ProvinciaId);


                query = query.Include(e => e.EmpresaDistribuidoraComunas);
                query = query.Where(e => e.EmpresaDistribuidoraComunas.Any(edc => comunasPorRegion.Any(cr => cr.Id == edc.ComunaId)));
            }

            if (request.ComunaId > 0)
            {
                query = query.Include(e => e.EmpresaDistribuidoraComunas);
                query = query.Where(e => e.EmpresaDistribuidoraComunas.Any(edc => edc.ComunaId == request.ComunaId));
            }



            return query;
        }

        public PagedList<EmpresaDistribuidoraListModel> Paged(EmpresaDistribuidoraRequest request)
        {
            _logger.LogInformation("Paged Empresa distribuidora");
            var queryModel = Query(request)
                .ProjectTo<EmpresaDistribuidoraListModel>(_mapper.ConfigurationProvider);
            return _repoEmpresaDistribuidora.Paged(queryModel, request.Page, request.Size);
        }

        public EmpresaDistribuidoraModel Get(long id)
        {
            var empresaDistribuidora = _repoEmpresaDistribuidora.Query().Include(e => e.Energetico).Where(ed => ed.Id == id).FirstOrDefault();


            return _mapper.Map<EmpresaDistribuidoraModel>(empresaDistribuidora);
        }

        public IEnumerable<EmpresaDistribuidoraModel> All()
        {
            return _repoEmpresaDistribuidora.All().ProjectTo<EmpresaDistribuidoraModel>(_mapper.ConfigurationProvider).OrderBy(a => a.Nombre).ToList();
        }

        public void Update(EmpresaDistribuidoraModel model)
        {
            var entityEmpresaDistribuidora = _repoEmpresaDistribuidora.Query().Where(e => e.Id == model.Id).FirstOrDefault();
            bool activo = entityEmpresaDistribuidora.Active;

            var toDB = _mapper.Map<EmpresaDistribuidora>(model);

            _repoEmpresaDistribuidora.DeleteComunas(model.Id);

            if (model.Comunas != null && model.Comunas.Count > 0)
                _repoEmpresaDistribuidora.AgregarComunas(model.Comunas, model.Id);

            toDB.Active = activo;

            _repoEmpresaDistribuidora.Update(toDB);
            _repoEmpresaDistribuidora.SaveChanges();

        }

        public long Insert(EmpresaDistribuidoraModel model)
        {
            var toBD = _mapper.Map<EmpresaDistribuidora>(model);
            
            _repoEmpresaDistribuidora.Insert(toBD);
          

            _repoEmpresaDistribuidora.AgregarComunas(model.Comunas, toBD.Id);

            return toBD.Id;
        }

        public void Delete(long id)
        {
            var empresaDistribuidora = _repoEmpresaDistribuidora.Get(id);
            if (empresaDistribuidora == null)
            {
                throw new NotFoundException(nameof(empresaDistribuidora));
            }

            _repoEmpresaDistribuidora.Delete(empresaDistribuidora);
            _repoEmpresaDistribuidora.SaveChanges();
        }

        public IEnumerable<EmpresaDistribuidoraModel> GetByEnergetico(long energeticoId)
        {
            return _repoEmpresaDistribuidora.GetByEnergetico(energeticoId)
                .ProjectTo<EmpresaDistribuidoraModel>(_mapper.ConfigurationProvider)
                .OrderBy(a => a.Nombre).ToList();
        }

        public IEnumerable<EmpresaDistribuidoraModel> GetByEnergeticoComuna(long energeticoId, long comunaId)
        {
            var empresaDistribuidora = _repoEmpresaDistribuidora.GetByEnergetico(energeticoId);

            empresaDistribuidora = empresaDistribuidora.Include(ec => ec.EmpresaDistribuidoraComunas);

            var porComuna = empresaDistribuidora.Where(ed => ed.EmpresaDistribuidoraComunas.Any(edc => edc.ComunaId == comunaId && edc.Active));

            return porComuna.ProjectTo<EmpresaDistribuidoraModel>(_mapper.ConfigurationProvider).OrderBy(a => a.Nombre).ToList();
        }
    }
}
