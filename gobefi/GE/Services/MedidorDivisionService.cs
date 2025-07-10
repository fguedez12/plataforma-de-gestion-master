using AutoMapper;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MedidorDivisionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class MedidorDivisionService : IMedidorDivisionService
    {
        private readonly IMedidorDivisionRepository _repoMedidorDivision;
        private readonly IMedidorRepository _repoMedidor;
        private readonly IMapper _mapper;

        protected readonly Usuario _usuario;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly UserManager<Usuario> _userManager;

        public MedidorDivisionService(
            IMedidorDivisionRepository  repoMedidorDivision,
            IMedidorRepository repoMedidor,
            IMapper mapper,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _repoMedidorDivision = repoMedidorDivision;
            _repoMedidor = repoMedidor;
            _mapper = mapper;
            _userManager = userManager;
            _usuario = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        public IEnumerable<MedidorDivisionSwitchModel> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public MedidorDivisionSwitchModel Get(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(MedidorDivisionSwitchModel model)
        {
            var entidadMD = _mapper.Map<MedidorDivision>(model);

            entidadMD.CreatedBy = _usuario.Id;
            entidadMD.CreatedAt = DateTime.Now;
            entidadMD.UpdatedAt = DateTime.MinValue;
            entidadMD.Active = true;

            _repoMedidorDivision.Insert(entidadMD);
            _repoMedidorDivision.SaveChanges();

            return entidadMD.Id;
        }

        public void Update(MedidorDivisionSwitchModel model)
        {
            var entidadOriginal = _repoMedidorDivision.Query().Where(md => md.MedidorId == model.MedidorId && md.DivisionId == model.DivisionId && md.Active).FirstOrDefault();

            if (entidadOriginal == null)
            {
                throw new Exception("relacion no existe para desactivar");
            }


            entidadOriginal.ModifiedBy = _usuario.Id;
            entidadOriginal.UpdatedAt = DateTime.Now;
            entidadOriginal.Active = false;

            _repoMedidorDivision.Update(entidadOriginal);
            _repoMedidorDivision.SaveChanges();
        }

        public void DeshabilitarDeDivision(long numeroClienteId, long divisionId)
        {
            var medidores = _repoMedidor.Query().Where(m => m.NumeroClienteId == numeroClienteId).ToList();
            var asociaciones = _repoMedidorDivision.Query().Where(md => md.DivisionId == divisionId && medidores.Exists(m => m.Id == md.MedidorId)).ToList();

            foreach (var asociacionItem in asociaciones)
            {
                asociacionItem.ModifiedBy = _usuario.Id;
                asociacionItem.UpdatedAt = DateTime.Now;
                asociacionItem.Active = false;

                _repoMedidorDivision.Update(asociacionItem);
                _repoMedidorDivision.SaveChanges();
            }
        }

        /// <summary>
        /// Deshabilita la relacion Medidor-Division
        /// </summary>
        /// <param name="medidorId"></param>
        /// <param name="divisionId"></param>
        public void Delete(long medidorId, long divisionId)
        {
            var medidorDivision = _repoMedidorDivision.Query().Where(md => md.MedidorId == medidorId && md.DivisionId == divisionId && md.Active).FirstOrDefault();

            medidorDivision.Active = false;
            medidorDivision.UpdatedAt = DateTime.Now;
            medidorDivision.ModifiedBy = _usuario.Id;


            _repoMedidorDivision.Update(medidorDivision);
            _repoMedidorDivision.SaveChanges();


            //Medidor medidorEntity = _repoMedidor.Get(id);

            //medidorEntity.Active = false;
            //medidorEntity.UpdatedAt = DateTime.Now;
            //medidorEntity.ModifiedBy = _usuario.Id;

            //_repoMedidor.Update(medidorEntity);

            ////medidorRepository.Delete(medidorEntity);
            //_repoMedidor.SaveChanges();
        }
    }
}
