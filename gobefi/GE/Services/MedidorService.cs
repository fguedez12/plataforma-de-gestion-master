using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using GE.Models.MedidorModels;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MedidorModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GobEfi.Web.Services
{
    public class MedidorService : IMedidorService
    {
        private readonly IMedidorRepository _repoMedidor;
        private readonly IDivisionRepository _repoDivision;
        private readonly IMedidorDivisionRepository _repoMedidorDivision;
        private readonly ICompraMedidorRepository _repoCompraMedidor;
        private readonly ILogger logger;
        private IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        protected readonly Usuario _usuario;
        protected readonly IHttpContextAccessor httpContextAccessor;

        public MedidorService(IMedidorRepository repoMedidor,
            IDivisionRepository repoDivision,
            IMedidorDivisionRepository repoMedidorDivision,
            ICompraMedidorRepository repoCompraMedidor,
            ILoggerFactory loggerFactory,
            IMapper mapper,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _repoMedidor = repoMedidor;
            _repoDivision = repoDivision;
            _repoMedidorDivision = repoMedidorDivision;
            _repoCompraMedidor = repoCompraMedidor;
            logger = loggerFactory.CreateLogger<MedidorService>();
            _mapper = mapper;
            _userManager = userManager;

            _usuario = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        public IEnumerable<MedidorModel> All()
        {
            return _repoMedidor.All().ProjectTo<MedidorModel>(_mapper.ConfigurationProvider);
        }

        /// <summary>
        /// Realiza eliminacion logica
        /// </summary>
        /// <param name="id">Id del Medidor a Eliminar</param>
        public void Delete(long id)
        {
            Medidor medidorEntity = _repoMedidor.Get(id);

            medidorEntity.Active = false;
            medidorEntity.UpdatedAt = DateTime.Now;
            medidorEntity.ModifiedBy = _usuario.Id;

            _repoMedidor.Update(medidorEntity);

            //medidorRepository.Delete(medidorEntity);
            _repoMedidor.SaveChanges();
        }

        public void DeleteByNumClienteId(long numClienteId)
        {
            List<Medidor> medidorEntitys = _repoMedidor.All().Where(x => x.NumeroClienteId == numClienteId).ToList();

            foreach (var item in medidorEntitys)
                Delete(item.Id);
                //medidorRepository.Delete(item);


            //medidorRepository.SaveChanges();
        }

        public MedidorModel Get(long id)
        {
            //var med = _repoMedidor.All().FirstOrDefault(x => x.Id == id);
            //var ret = _mapper.Map<MedidorModel>(med);
            var ret = _repoMedidor.All().ProjectTo<MedidorModel>(_mapper.ConfigurationProvider).FirstOrDefault(x => x.Id == id);

            return ret;
        }

        //public IEnumerable<MedidorModel> Get(long divisionId, long EnergeticoDivisionId)
        //{
        //    var ret = medidorRepository.Get(divisionId, EnergeticoDivisionId).ProjectTo<MedidorModel>(mapper.ConfigurationProvider);

        //    return ret;
        //}

        public List<MedidorModel> GetByNumClienteId(long id)
        {
            var ret = _repoMedidor.GetByNumClienteId(id).ProjectTo<MedidorModel>(_mapper.ConfigurationProvider);

            return ret.ToList();
        }

        public List<MedidorModel> GetByNumMedidor(long numMedidor)
        {
            var ret = _repoMedidor.GetByNumMedidor(numMedidor).ProjectTo<MedidorModel>(_mapper.ConfigurationProvider);

            return ret.ToList();
        }

        public List<MedidorModel> GetByDivisionId(long divisionId)
        {
            var medidores = _repoMedidorDivision.Query()
                .Where(md => md.DivisionId == divisionId && md.Active)
                .Include(md => md.Medidor)
                .Include(md=>md.Division)
                .ThenInclude(d=>d.Servicio)
                .ThenInclude(s=>s.Institucion)
                .Select(md => md.Medidor);

            return medidores.ProjectTo<MedidorModel>(_mapper.ConfigurationProvider).ToList();
            //var ret = _repoMedidor.GetByNumClientesIds(numClientesIds).ProjectTo<MedidorModel>(_mapper.ConfigurationProvider).ToList();

            //return ret;
        }

        public long Insert(MedidorModel model)
        {

            var medidorList = _repoMedidor.GetByNumClienteId(model.NumeroClienteId);

            if (medidorList.Any(m => m.Numero == model.Numero)) {

                //throw new Exception("El numero de medidor ya existe para el numero de cliente");

                throw new Newtonsoft.Json.JsonException(Newtonsoft.Json.JsonConvert.SerializeObject(new { MedidorDuplicado = "El numero de medidor ya existe para el numero de cliente" }));
            }

            Medidor medidor = this._mapper.Map<Medidor>(model);
            medidor.Numero = medidor.Numero.Trim();
            medidor.CreatedBy = _usuario.Id;

            this._repoMedidor.Insert(medidor);
            this._repoMedidor.SaveChanges();


            return medidor.Id;
        }

        public void Update(MedidorModel model)
        {
            var medidorOriginal = _repoMedidor.Query().Where(m => m.Id == model.Id).FirstOrDefault();

            var entity = _mapper.Map<Medidor>(model);

            entity.Active = medidorOriginal.Active;
            entity.UpdatedAt = DateTime.Now;
            entity.NumeroCliente = null;
            entity.ModifiedBy = _usuario.Id;
            entity.Version = ++entity.Version;

            _repoMedidor.Update(entity);
            _repoMedidor.SaveChanges();
        }

        public async Task<IEnumerable<MedidorToDDL>> ByNumeroCliente(long numeroClienteId)
        {
            var numClientesToServices = await _repoMedidor.Query()
                .Where(ed => ed.NumeroClienteId == numeroClienteId && ed.Active)
                .Include(n => n.NumeroCliente)
                .Select(e => new MedidorToDDL{
                    Id = e.Id,
                    Numero = e.Numero,
                    Nombre = e.Numero,
                    NumeroClienteId = e.NumeroClienteId
                }).Distinct().ToListAsync();

            return numClientesToServices;
        }

        public async Task<MedidorModel> GetByNumClienteIdAndNumMedidor(MedidorParametrosModel parametroMedidor)
        {
            var edificioActual = await _repoDivision.Query().Where(d => d.Id == Convert.ToInt64(parametroMedidor.DivisionId)).Include(d => d.Edificio).Select(d => d.Edificio).FirstOrDefaultAsync();
            var medidorQuery = _repoMedidor.Query().Where(m => m.Numero == parametroMedidor.NumeroMedidor);

            if (await medidorQuery.FirstOrDefaultAsync() == null)
            {
                return null;
            }

            var numClienteQuery = medidorQuery.Include(m => m.NumeroCliente).Select(m => m.NumeroCliente);

            numClienteQuery = numClienteQuery.Where(nc => nc.Id == Convert.ToInt64(parametroMedidor.NumeroClienteId));

            if (await numClienteQuery.FirstOrDefaultAsync() == null)
            {
                return null;
            }

            var empresaDistribuidoraQuery = numClienteQuery.Include(nc => nc.EmpresaDistribuidora).Select(nc => nc.EmpresaDistribuidora);

            var empresaDistribuidoraComunasQuery = empresaDistribuidoraQuery.Include(ed => ed.EmpresaDistribuidoraComunas).Select(nc => nc.EmpresaDistribuidoraComunas);

            //bool mismaComuna = empresaDistribuidoraComunasQuery.Where(s => s.)

            return new MedidorModel();

            
        }

        public bool EstanAsociado(long medidorId, long divisionId)
        {
            var relacion = _repoMedidorDivision.Query().Where(md => md.MedidorId == medidorId && md.DivisionId == divisionId && md.Active).FirstOrDefault();

            if (relacion == null)
                return false;

            return true;
        }

        public async Task<IEnumerable<MedidorToDDL>> ByNumClienteIdDivisionId(long numClienteId, long divisionId)
        {
            var medidorDivision = _repoMedidorDivision.Query().Where(md => md.DivisionId == divisionId && md.Active);

            var medidores = medidorDivision.Include(md => md.Medidor).Select(md => md.Medidor);

            var medidorToDDLs = await medidores.Where(m => m.NumeroClienteId == numClienteId && m.Active).Select(m => new MedidorToDDL
            {

                Id = m.Id,
                Numero = m.Numero,
                NumeroClienteId = m.NumeroClienteId,
                DivisionId = m.DivisionId

            }).Distinct().ToListAsync();

            //var numClientesToServices = await _repoMedidor.Query()
            //   .Where(ed => ed.NumeroClienteId == numeroClienteId && ed.Active)
            //   .Include(n => n.NumeroCliente)
            //   .Select(e => new MedidorToDDL
            //   {
            //       Id = e.Id,
            //       Numero = e.Numero,
            //       NumeroClienteId = e.NumeroClienteId
            //   }).Distinct().ToListAsync();

            return medidorToDDLs;
        }

        public async Task<IEnumerable<MedidorToDDL>> GetByCompraId(long compraId)
        {
            var medidores = _repoCompraMedidor.Query().Where(cm => cm.CompraId == compraId).Select(cm => cm.Medidor);

            var medidorToDDLs = await medidores.Select(m => new MedidorToDDL
            {

                Id = m.Id,
                Numero = m.Numero,
                NumeroClienteId = m.NumeroClienteId
            }).Distinct().ToListAsync();

            return medidorToDDLs;

        }

        public void DesasociarbyNumCliente(long numClienteId, long divisionId)
        {
            var medidores = _repoMedidor.Query().Where(m => m.NumeroClienteId == numClienteId).ToList();
            
            var medidoresDivisiones = _repoMedidorDivision.Query().Where(md => md.DivisionId == divisionId && medidores.Any(m => m.Id == md.MedidorId) && md.Active).ToList();

            foreach (var itemMedidorDivision in medidoresDivisiones)
            {
                itemMedidorDivision.Active = false;
                _repoMedidorDivision.Update(itemMedidorDivision);

            }

            _repoMedidorDivision.SaveChanges();

        }
    }
}
