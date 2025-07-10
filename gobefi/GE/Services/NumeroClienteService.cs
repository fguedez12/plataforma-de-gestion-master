using AutoMapper;
using AutoMapper.QueryableExtensions;
using GE.Models.NumeroClienteModels;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Core.Extensions;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.NumeroClienteModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class NumeroClienteService : INumeroClienteService
    {
        private INumeroClienteRepository _repoNumCliente;
        private IEnergeticoDivisionRepository energeticoDivRepository;
        private IDivisionRepository _divisionRepository;
        private IEmpresaDistribuidoraRepository empresaDistribuidoraRepository;
        private ITipoTarifaRepository tipoTarifaRepository;
        private readonly IMedidorRepository _repoMedidor;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        protected readonly Usuario _usuario;
        private readonly ILogger logger;
        private IMapper mapper;

        public NumeroClienteService(ILoggerFactory loggerFactory,
            IMapper mapper,
            INumeroClienteRepository repoNumCliente,
            IEnergeticoDivisionRepository energeticoDivRepository,
            IDivisionRepository divisionRepository,
            IEmpresaDistribuidoraRepository empresaDistribuidoraRepository,
            ITipoTarifaRepository tipoTarifaRepository,
            IMedidorRepository repoMedidor,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this._repoNumCliente = repoNumCliente;
            this.energeticoDivRepository = energeticoDivRepository;
            this.empresaDistribuidoraRepository = empresaDistribuidoraRepository;
            this.tipoTarifaRepository = tipoTarifaRepository;
            _divisionRepository = divisionRepository;
            _repoMedidor = repoMedidor;
            _userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = loggerFactory.CreateLogger<NumeroClienteService>();
            this.mapper = mapper;
            _userManager = userManager;

            _usuario = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        public IEnumerable<NumeroClienteModel> All()
        {
            return _repoNumCliente
                .All().OrderBy(o => o.Id).ProjectTo<NumeroClienteModel>(mapper.ConfigurationProvider).ToList();
        }

        public void Delete(long id)
        {
            NumeroCliente nCliente = _repoNumCliente.Get(id);

            nCliente.Active = false;
            nCliente.UpdatedAt = DateTime.Now;
            nCliente.ModifiedBy = _usuario.Id;
            //numeroClienteRepository.Delete(nCliente);

            _repoNumCliente.Update(nCliente);
            _repoNumCliente.SaveChanges();
        }

        public NumeroClienteModel Get(long id)
        {
            return mapper.Map<NumeroClienteModel>(_repoNumCliente.Get(id));
        }

        public NumeroClienteModel GetByNumero(string numeroCliente)
        {
            var listEnity = mapper.Map<NumeroClienteModel>(_repoNumCliente.GetByNumero(numeroCliente));

            return listEnity;
        }

        public bool NumClientExist(string numeroCliente, long divisionId, long energeticoDivisionId)
        {
            if (_repoNumCliente.NumClientExist(numeroCliente, divisionId, energeticoDivisionId))
                return true;

            return false;
        }

        /// <summary>
        /// Obtiene la lista de Numero de Clientes de la division y del energetico seleccionado
        /// </summary>
        /// <param name="divisionId">Id de la Division</param>
        /// <param name="EnergeticoDivisionId">Id de</param>
        /// <returns></returns>
        public IEnumerable<NumeroClienteModel> Get(long divisionId, long EnergeticoId)
        {
            List<long> numeroDeClientes = energeticoDivRepository.Get(divisionId, EnergeticoId);
            List<NumeroClienteModel> ret = new List<NumeroClienteModel>();


            foreach (long Id in numeroDeClientes)
            {
                if (Id == 0)
                    continue;

                NumeroCliente NumCliente = _repoNumCliente.Get(Id);
                if (!NumCliente.Active)
                    continue;

                NumCliente.EmpresaDistribuidora = empresaDistribuidoraRepository.Get(NumCliente.EmpresaDistribuidoraId._toLong());

                var tipoTarifa = tipoTarifaRepository.Get(NumCliente.TipoTarifaId ?? 0);

                NumCliente.TipoTarifa = tipoTarifa ?? new TipoTarifa { Nombre = "" };

                NumeroClienteModel modelNumCliente = mapper.Map<NumeroClienteModel>(NumCliente);

                modelNumCliente.NumMedidoresExclusivos = _repoMedidor.Query().Where(nc => nc.NumeroClienteId == Id && nc.Active).Where(c => !c.Compartido).Count();
                modelNumCliente.NumMedidoresCompartidos = _repoMedidor.Query().Where(nc => nc.NumeroClienteId == Id && nc.Active).Where(c => c.Compartido).Count();
                modelNumCliente.TotalMedidores = _repoMedidor.Query().Where(nc => nc.NumeroClienteId == Id && nc.Active).Count();


                ret.Add(modelNumCliente);
            }

            //var ret = numeroClienteRepository.Get(divisionId, EnergeticoId).ProjectTo<NumeroClienteModel>(mapper.ConfigurationProvider).ToList();

            return ret;
        }

        /// <summary>
        /// Inserta un nuevo numero de cliente
        /// si el numero de cliente existe retorna -1
        /// si se inserto sin problemas retorna el id del nuevo numero de cliente
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Insert(NumeroClienteModel model)
        {
            bool exist = _repoNumCliente.Query().Any(nc => nc.Numero == model.Numero && nc.EmpresaDistribuidoraId == model.EmpresaDistribuidoraId);

            if (exist)
                return -1;


            NumeroCliente NClient = this.mapper.Map<NumeroCliente>(model);

            model.Numero = model.Numero.Trim();
            model.CreatedBy = _usuario.Id;

            this._repoNumCliente.Insert(NClient);
            this._repoNumCliente.SaveChanges();

            return NClient.Id;

        }

        public void Update(NumeroClienteModel model)
        {
            NumeroCliente entity = this.mapper.Map<NumeroCliente>(model);

            entity.Active = true;
            entity.Numero = model.Numero.Trim().Replace(" ", "");
            entity.UpdatedAt = DateTime.Now;
            entity.ModifiedBy = _usuario.Id;

            _repoNumCliente.Update(entity);
            _repoNumCliente.SaveChanges();
        }

        public async Task<NumeroClienteModel> GetNumeroClientesByNum(string numCliente, long? empresaDistribuidoraId)
        {

            NumeroCliente modelFromRepository = await _repoNumCliente.Query()
                .Where(x => x.Numero == numCliente && x.EmpresaDistribuidoraId == empresaDistribuidoraId)
                .FirstOrDefaultAsync();

            NumeroClienteModel modelToReturn = mapper.Map<NumeroClienteModel>(modelFromRepository);

            return modelToReturn;
        }

        public async Task<ICollection<NumClienteToDDL>> ByDivision(long divisionId, long energeticoId)
        {
            //var energeticoDivision =  energeticoDivRepository.Query()
            //    .Where(ed => ed.DivisionId == divisionId && ed.NumeroClienteId != null && ed.Active && ed.EnergeticoId == energeticoId);

            //var medidores = _repoMedidor.Query()
            //    .Where(m => energeticoDivision.Any(ed => ed.NumeroClienteId == m.NumeroClienteId) && m.DivisionId == divisionId);

            //var numClientesToServices = await medidores.Include(m => m.NumeroCliente)
            //    .Select(e => new NumClienteToDDL {
            //        Id = e.NumeroCliente.Id,
            //        Numero = e.NumeroCliente.Numero,
            //        Nombre = e.NumeroCliente.Numero,
            //        EnergeticoId = energeticoId
            //    }).Distinct().ToListAsync(); ;

            var numClientesToServices = await energeticoDivRepository.Query()
                .Include(n => n.NumeroCliente)
                .Where(ed => ed.DivisionId == divisionId && ed.NumeroClienteId != null && ed.Active && ed.EnergeticoId == energeticoId)
                .Select(e => new NumClienteToDDL
                {
                    Id = e.NumeroCliente.Id,
                    Numero = e.NumeroCliente.Numero,
                    Nombre = e.NumeroCliente.Numero,
                    EnergeticoId = e.EnergeticoId
                }).Distinct().ToListAsync();

            return numClientesToServices;
        }

        public bool validaNumeroByMedidor(long divisionId, long energeticoId, long numeroClienteId)
        {
            bool result = false;

            var energeticoDivision = energeticoDivRepository.Query()
                .Where(ed => ed.DivisionId == divisionId && ed.NumeroClienteId != null && ed.Active && ed.EnergeticoId == energeticoId);

            var medidores = _repoMedidor.Query()
                .Where(m => energeticoDivision.Any(ed => ed.NumeroClienteId == m.NumeroClienteId) && m.DivisionId == divisionId);

            var numClientesToServices = medidores.Include(m => m.NumeroCliente)
                .Where(m => m.NumeroClienteId == numeroClienteId)
                .Select(n => n.NumeroClienteId).ToList();

            result = numClientesToServices.Count > 0 ?  true :  false;

            return result;
        }

        public async Task<ICollection<NumClienteToDDL>> ByEdificioId(long edificioId, long energeticoId)
        {

            var divisiones = _divisionRepository.Query().Where(d => d.EdificioId == edificioId).ToList();

            List<NumClienteToDDL> numClienteModel = new List<NumClienteToDDL>();

            foreach (var div in divisiones) {

                var energeticoDiv = energeticoDivRepository.Query().Where(ed => ed.DivisionId == div.Id && ed.EnergeticoId == energeticoId && ed.Active && ed.NumeroClienteId!=null);

                var nCliente = energeticoDiv.Include(ed => ed.NumeroCliente).Select(ed => ed.NumeroCliente);

                var nClienteModel = await nCliente.ProjectTo<NumClienteToDDL>(mapper.ConfigurationProvider).ToListAsync();

                foreach (var itemNcliente in nClienteModel)
                {
                    numClienteModel.Add(itemNcliente);

                }

            }

            return numClienteModel;
        }
    }
}
