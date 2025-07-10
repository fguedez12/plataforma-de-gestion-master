using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoDivisionModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.UnidadMedidaModels;
using GobEfi.Web.Services.Request;
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
    public class EnergeticoService : IEnergeticoService
    {
        private IEnergeticoRepository _repoEnergetico;
        private readonly IEnergeticoDivisionRepository _repoEnergeticoDivision;
        private readonly IEnergeticoUnidadMedidaRepository _repoEnergeticoUnidadesMedida;
        private readonly IDivisionRepository _repoDivision;
        private readonly IEdificioRepository _repoEdificio;
        private readonly ILogger logger;
        private IMapper _mapper;
        protected readonly Usuario _usuario;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly UserManager<Usuario> _userManager;

        public EnergeticoService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IEnergeticoRepository energeticoRepository,
            IEnergeticoDivisionRepository repoEnergeticoDivision,
            IEnergeticoUnidadMedidaRepository repoEnergeticosUnidadesMedida,
            IDivisionRepository repoDivision,
            IEdificioRepository repoEdificio,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _repoEnergetico = energeticoRepository;
            _repoEnergeticoDivision = repoEnergeticoDivision;
            _repoEnergeticoUnidadesMedida = repoEnergeticosUnidadesMedida;
            _repoDivision = repoDivision;
            _repoEdificio = repoEdificio;
            this.logger = loggerFactory.CreateLogger<DivisionService>();
            _mapper = mapper;
            _userManager = userManager;

            _usuario = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        private IQueryable<Energetico> Query(EnergeticoRequest request)
        {
            var query = _repoEnergetico.Query();

            if (!string.IsNullOrEmpty(request.Nombre))
            {
                query = query.Where(u => EF.Functions.Like(u.Nombre, $"%{request.Nombre}%"));
            }

            if (!string.IsNullOrEmpty(request.FieldName))
            {
                switch (request.FieldName)
                {
                    case "id":
                        query = query.OrderBy(x => x.Id);
                        break;
                    case "nombre":
                        query = query.OrderBy(x => x.Nombre);
                        break;
                }
            }

            return query;
        }

        public IEnumerable<EnergeticoModel> All()
        {
            return _repoEnergetico.All().ProjectTo<EnergeticoModel>(_mapper.ConfigurationProvider).OrderBy(a => a.Posicion).ToList();
        }

        public void Delete(long id)
        {
            var paraEliminar = _repoEnergetico.Get(id);
            paraEliminar.ModifiedBy = _usuario.Id;
            _repoEnergetico.Delete(paraEliminar);
            _repoEnergetico.SaveChanges();
        }

        public EnergeticoModel Get(long id)
        {
            Energetico ener = _repoEnergetico.Get(id);

            object objEner = this._mapper.Map(ener, typeof(Energetico), typeof(EnergeticoModel));

            return (EnergeticoModel) objEner;
        }

        public long Insert(EnergeticoModel model)
        {
            throw new NotImplementedException();
        }

        public PagedList<EnergeticoListModel> Paged(EnergeticoRequest request)
        {
            logger.LogInformation("Paged DivisionList");
            var queryModel = Query(request).ProjectTo<EnergeticoListModel>(_mapper.ConfigurationProvider);
            return _repoEnergetico.Paged(queryModel, request.Page, request.Size);
        }

        public IEnumerable<EnergeticoSelectModel> Select()
        {
            return _repoEnergetico
                    .All()
                    .OrderBy(o => o.Nombre)
                    .ProjectTo<EnergeticoSelectModel>()
                    .ToList();
        }

        public void Update(EnergeticoModel model)
        {
            var energetico = _repoEnergetico.Get(model.Id);
            if (energetico == null)
            {
                throw new NotFoundException(nameof(energetico));
            }

            _mapper.Map<EnergeticoModel, Energetico>(model, energetico);
            energetico.ModifiedBy = _usuario.Id;

            _repoEnergetico.Update(energetico);
            _repoEnergetico.SaveChanges();
        }

        public EnergeticoModel CheckFirstEnergetico(long numeroClienteId, long energeticoIdActual)
        {

            var energetico = _mapper.Map<EnergeticoModel>(_repoEnergetico.CheckFirstEnergetico(numeroClienteId, energeticoIdActual));

            return energetico;
        }


        public async Task<IEnumerable<EnergeticoActivoModel>> GetActivosByDivision(long divisionId)
        {
            var energeticosActivos = await _repoEnergeticoDivision.Query()
                                            .Where(ed => ed.DivisionId == divisionId && ed.Active && ed.NumeroClienteId == null)
                                            .Include(ed => ed.Energetico)
                                            .Select(n => new EnergeticoActivoModel
                                            {
                                                Id = n.Energetico.Id,
                                                Nombre = n.Energetico.Nombre
                                            }).Distinct().ToListAsync();

            var energeticoUniMedida = _repoEnergeticoUnidadesMedida.Query();

            foreach (var item in energeticosActivos)
            {
                item.UnidadesMedida = energeticoUniMedida.Where(e => e.EnergeticoId == item.Id && e.Active)
                .Include(u => u.UnidadMedida).Select(u => new UnidadMedidaModel
                {
                    Id = u.UnidadMedida.Id,
                    Nombre = u.UnidadMedida.Nombre,
                    Abreviacion = u.UnidadMedida.Abrv
                }).ToList();

                // valida si el energetico asociado a la division tiene numero de cliente
                var tieneNumCliente = _repoEnergeticoDivision.Query().Where(a => a.DivisionId == divisionId
                && a.Active
                && a.NumeroClienteId != null
                && a.EnergeticoId == item.Id).Count() > 0;

                item.TieneNumCliente = tieneNumCliente;
            }

            return energeticosActivos;

        }

        public async Task<EnergeticoForEditModel> GetForEdit(long? id)
        {
            var energeticos = await _repoEnergetico.Query().Where(e => e.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<EnergeticoForEditModel>(energeticos);
        }

        public async Task UpdateAsync(EnergeticoForEditModel energeticoModel)
        {
            var energetico = await _repoEnergetico.All().Where(e => e.Id == energeticoModel.Id).Include(e => e.EnergeticoUnMedida).FirstOrDefaultAsync();
            if (energetico == null)
            {
                throw new NotFoundException(nameof(energetico));
            }

            _mapper.Map<EnergeticoForEditModel, Energetico>(energeticoModel, energetico);

            energetico.EnergeticoUnMedida.ToList().ForEach(e => e.Active = false);

            foreach (var item in energeticoModel.UnidadesDeMedidasId)
            {
                var energeticoUM = energetico.EnergeticoUnMedida.FirstOrDefault(eum => eum.EnergeticoId == energetico.Id && eum.UnidadMedidaId == item);

                if (energeticoUM != null)
                    energeticoUM.Active = true;

            }

            _repoEnergetico.Update(energetico);
            _repoEnergetico.SaveChanges();
        }

        public async Task<IEnumerable<EnergeticoDivisionModel>> GetByDivisionId(long divisionId)
        {
            var result = _repoEnergeticoDivision.Query().Where(ed => ed.DivisionId == divisionId && ed.Active && ed.NumeroClienteId == null).Distinct();

            return await result.ProjectTo<EnergeticoDivisionModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<EnergeticoModel>> GetAllAsync()
        {
            return await _repoEnergetico.All().ProjectTo<EnergeticoModel>(_mapper.ConfigurationProvider).OrderBy(a => a.Posicion).ToListAsync();
        }

        public async Task InsertAsync(EnergeticoModel energeticoModel)
        {
            var buscandoReporteById = await _repoEnergetico.Query().Where(e => e.Id == energeticoModel.Id).FirstOrDefaultAsync();

            if (buscandoReporteById != null)
                throw new Exception("el energetico ya existe");


            var energetico = _mapper.Map<Energetico>(energeticoModel);
            energetico.CreatedBy = _usuario.Id;
            _repoEnergetico.Insert(energetico);
            _repoEnergetico.SaveChanges();
        }

        public async Task<IEnumerable<EnergeticoActivoModel>> GetByEdificioId(long edificioId)
        {
            var divisiones = _repoDivision.Query().Where(d => d.EdificioId == edificioId).ToList();

            List<EnergeticoActivoModel> energeticosModels = new List<EnergeticoActivoModel>();
            foreach (var itemDivision in divisiones)
            {
                var energeticoDivis = _repoEnergeticoDivision.Query().Where(ed => ed.DivisionId == itemDivision.Id && ed.Active && ed.NumeroClienteId == null);

                var energetico =  energeticoDivis.Include(ed => ed.Energetico).Select(ed => ed.Energetico);

                var energeticoModel = await energetico.ProjectTo<EnergeticoActivoModel>(_mapper.ConfigurationProvider).ToListAsync();

                foreach(var itemEnergetico in energeticoModel)
                    if (!energeticosModels.Exists(e => e.Id == itemEnergetico.Id))
                        energeticosModels.Add(itemEnergetico);
            }
            

            return energeticosModels;
        }
    }
}
