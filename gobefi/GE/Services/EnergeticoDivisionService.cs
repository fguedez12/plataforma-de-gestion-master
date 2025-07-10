using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoDivisionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GobEfi.Web.Services
{
    public class EnergeticoDivisionService : IEnergeticoDivisionService
    {
        private IEnergeticoDivisionRepository _repoEnergeticoDivision;
        private IMedidorRepository medidorRepository;
        private readonly IMedidorDivisionService _servMedidorDivision;
        private readonly ILogger logger;
        private IMapper mapper;

        private UserManager<Usuario> _userManager;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly Usuario _usuario;



        public EnergeticoDivisionService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IEnergeticoDivisionRepository repoEnergeticoDivision,
            IMedidorRepository medidorRepository,
            IMedidorDivisionService servMedidorDivision,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _repoEnergeticoDivision = repoEnergeticoDivision;
            this.medidorRepository = medidorRepository;
            _servMedidorDivision = servMedidorDivision;
            this.logger = loggerFactory.CreateLogger<EnergeticoDivisionService>();
            this.mapper = mapper;
            this._userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;

            _usuario = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        public IEnumerable<EnergeticoDivisionModel> All()
        {
            throw new System.NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public EnergeticoDivisionModel Get(long id)
        {
            throw new System.NotImplementedException();
        }


        public long Insert(EnergeticoDivisionModel model)
        {
            long ret = 0;

            try
            {
                EnergeticoDivision relacionEnergeticoDivision = mapper.Map<EnergeticoDivision>(model);

                relacionEnergeticoDivision.CreatedBy = _usuario.Id;
                relacionEnergeticoDivision.CreatedAt = DateTime.Now;

                _repoEnergeticoDivision.Insert(relacionEnergeticoDivision);

                _repoEnergeticoDivision.SaveChanges();

                ret = relacionEnergeticoDivision.Id;
            }
            catch
            {
                throw new Exception($"Error al asociar numero de cliente {model.NumeroClienteId}.");
            }

            return ret; 
        }

        public void Update(EnergeticoDivisionModel model)
        {
            var entity = mapper.Map<EnergeticoDivision>(model);

            entity.ModifiedBy = _usuario.Id;
            entity.UpdatedAt = DateTime.Now;
            entity.Active = false;

            _repoEnergeticoDivision.Update(entity);
            _repoEnergeticoDivision.SaveChanges();

        }

        public IList<EnergeticoDivision> GetByDivisionId(long id)
        {
            return _repoEnergeticoDivision.GetByDivisionId(id);
        }

        /// <summary>
        /// Desactiva la asociacion entre el numero de cliente y la relacion division-energetico 
        /// Valida si el numero de cliente desactivado se encuentra asociado a mas de una relacion division-energetico
        /// de no estar asociado a una o ninguna relacion, modifica el campo "Compartido" de los medidores relacionados con al numero de cliente
        /// </summary>
        /// <param name="NClienteId">Numero de cliente Id a desactivar</param>
        /// <param name="divisionId">Division Id actual</param>
        /// <param name="energeticoId">Energetico Id actual</param>
        public void Delete(long NClienteId, long divisionId, long energeticoId)
        {
            EnergeticoDivision toDelete = _repoEnergeticoDivision.Get(NClienteId, divisionId, energeticoId);

            toDelete.Active = false;
            toDelete.UpdatedAt = DateTime.Now;

            _repoEnergeticoDivision.Update(toDelete);
            _repoEnergeticoDivision.SaveChanges();

            _servMedidorDivision.DeshabilitarDeDivision(NClienteId, divisionId);

            //revisar si el numero de cliente esta asociado a mas relaciones unidad-division que este activa
            if (!_repoEnergeticoDivision.TieneMasAsociado(NClienteId))
                return;

            medidorRepository.CambiaEstadoCompartido(NClienteId, false);

            //DeshabilitarDeDivision
        }

        public IList<EnergeticoDivisionModel> Get(long DivisionId, long EnergeticoId)
        {
            var fromRepository = _repoEnergeticoDivision.Get(DivisionId, EnergeticoId, true);

            var toView = mapper.Map<IList<EnergeticoDivisionModel>>(fromRepository);

            return toView;
        }

        public bool ExisteRelacion(long divisionId, long energeticoId, long numeroClienteId)
        {
            var relacion = _repoEnergeticoDivision.Query().Where(ed => ed.DivisionId == divisionId && ed.EnergeticoId == energeticoId && ed.NumeroClienteId == numeroClienteId && ed.Active).FirstOrDefault();

            return relacion != null;

        }

        public bool TieneNumCliente(long divisionId, long? energeticoId) {

            var nClientes = _repoEnergeticoDivision.Query().Where(ed => ed.DivisionId == divisionId && ed.EnergeticoId==energeticoId && ed.Active == true && ed.NumeroClienteId !=null);

            return nClientes.Count() > 0 ? true : false;
        }
    }
}
