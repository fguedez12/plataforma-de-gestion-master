using AutoMapper;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Helper;
using GobEfi.Web.Models.MuroModels;
using GobEfi.Web.Models.PisoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class MuroService : IMuroService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly Usuario _currentUser;

        public MuroService(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }
        public async Task<List<PisoModel>> SaveMuros(MurosForSave murosForSave) 
        {

            var pisoList = new List<Piso>();

            for (int i = 0; i < murosForSave.pisos.Length; i++)
            {
                var pisoFromDb = _context.Pisos.Where(p => p.Id == murosForSave.pisos[i].Id)
                    .Include(p => p.NumeroPiso)
                    .Include(p=>p.Muros)
                    .FirstOrDefault();

                pisoFromDb.Muros = pisoFromDb.Muros.Where(m => m.Active).ToList();

                if (murosForSave.Level != null)
                {
                    if (i == murosForSave.Level)
                    {
                        pisoFromDb.Muros = new List<Muro>();
                        foreach (var muro in murosForSave.pisos[i].Muros)
                        {
                            var newMuro = _mapper.Map<Muro>(muro);
                            
                            newMuro.Active = true;
                            newMuro.CreatedAt = DateTime.Now;
                            newMuro.Version = 1;
                            newMuro.CreatedBy = _currentUser.Id;
                            newMuro.FachadaEste = FachadaEste(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                            newMuro.FachadaNorte = FachadaNorte(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                            newMuro.FachadaOeste = FachadaOeste(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                            newMuro.FachadaSur = FachadaSur(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                            pisoFromDb.Muros.Add(newMuro);
                        }
                    }
                }
                else {

                    //pisoFromDb.FrontisIndex = piso.FrontisIndex;
                    if (pisoFromDb.Muros == null)
                    {
                        pisoFromDb.Muros = new List<Muro>();
                    }
                    foreach (var muro in murosForSave.pisos[i].Muros)
                    {
                        var newMuro = _mapper.Map<Muro>(muro);
                        newMuro.FachadaEste = FachadaEste(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                        newMuro.FachadaNorte = FachadaNorte(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                        newMuro.FachadaOeste = FachadaOeste(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                        newMuro.FachadaSur = FachadaSur(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                        newMuro.Active = true;
                        newMuro.CreatedAt = DateTime.Now;
                        newMuro.Version = 1;
                        newMuro.CreatedBy = _currentUser.Id;
                        pisoFromDb.Muros.Add(newMuro);
                    }
                }

                pisoList.Add(pisoFromDb);

            }
          
            try
            {
                await _context.SaveChangesAsync();

                var edificio = _context.Pisos
                 .Where(p => p.Id == murosForSave.pisos[0].Id)
                 .Include(p => p.Edificio).Select(p => p.Edificio).FirstOrDefault();

                if (edificio.FrontisId == null)
                {
                    var muroList = pisoList[0].Muros.OrderBy(m=>m.Id).ToList();
                    edificio.FrontisId = muroList[0].Id;

                }

                await _context.SaveChangesAsync();

                var result = _mapper.Map<List<PisoModel>>(pisoList);

                return result;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<List<PisoModel>> SaveMurosInternos(MurosForSave murosForSave)
        {
            var pisoList = new List<Piso>();
            foreach (var piso in murosForSave.pisos)
            {
                var pisoFromDb = _context.Pisos.Where(p => p.Id == piso.Id)
                    .Include(p => p.NumeroPiso)
                    .Include(p => p.Muros)
                    .FirstOrDefault();

                pisoFromDb.Muros = pisoFromDb.Muros.Where(m => m.Active).ToList();

                //pisoFromDb.FrontisIndex = piso.FrontisIndex;
                if (pisoFromDb.Muros == null)
                {
                    pisoFromDb.Muros = new List<Muro>();
                }
                foreach (var muro in piso.Muros)
                {
                    if (muro.Tipo == EnumHelper.GetEnumDisplayName(TipoMuro.Interno))
                    {
                        var newMuro = _mapper.Map<Muro>(muro);
                        newMuro.FachadaEste = FachadaEste(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                        newMuro.FachadaNorte = FachadaNorte(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                        newMuro.FachadaOeste = FachadaOeste(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                        newMuro.FachadaSur = FachadaSur(newMuro.Azimut, newMuro.Distancia, pisoFromDb.Altura);
                        newMuro.Active = true;
                        newMuro.CreatedAt = DateTime.Now;
                        newMuro.Version = 1;
                        newMuro.CreatedBy = _currentUser.Id;
                        pisoFromDb.Muros.Add(newMuro);
                    }

                }
                pisoList.Add(pisoFromDb);
            }
            try
            {
                await _context.SaveChangesAsync();
                var result = _mapper.Map<List<PisoModel>>(pisoList);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }


        public async Task DisableMuros(MurosForSave murosForSave)
        {
            foreach (var piso in murosForSave.pisos)
            {
                var pisoFromDb = _context.Pisos
                                .Where(p => p.Id == piso.Id)
                                .Include(p=>p.Muros)
                                .FirstOrDefault();
                
                foreach (var muro in pisoFromDb.Muros)
                {
                    muro.Active = false;
                    muro.ModifiedBy = _currentUser.Id;
                    muro.UpdatedAt = DateTime.Now;
                }
            }

            var edificio = _context.Pisos
                    .Where(p => p.Id == murosForSave.pisos[0].Id)
                    .Include(p => p.Edificio)
                    .Select(p => p.Edificio).FirstOrDefault();
            edificio.FrontisId = null;
            _context.Entry(edificio).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            //await Task.FromResult(1);
        }

        public async Task DisableMurosByPiso(long pisoId)
        {
            var pisoFomDb = await _context.Pisos.Where(p => p.Id == pisoId).Include(p=>p.Muros).FirstOrDefaultAsync();

            pisoFomDb.Muros = pisoFomDb.Muros.Where(m => m.Active).ToList();

            foreach (var muro in pisoFomDb.Muros)
            {
                muro.Active = false;
                muro.Version = muro.Version + 1;
                muro.UpdatedAt = DateTime.Now;
                muro.ModifiedBy = _currentUser.Id;
                _context.Entry(muro).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateMuro(long id, MuroModel muro)
        {
            

           

            if (id != muro.Id)
            {
                throw new Exception("El id no corresponde con el muro seleccionado");
            }

            var muroFromDb = await _context.Muros.Where(m => m.Id == id).FirstOrDefaultAsync();
            var pisoFomDb = await _context.Pisos.Where(p => p.Id == muroFromDb.PisoId).FirstOrDefaultAsync();

            if (muro.TipoSombreado is null)
            {


            }
            else {
                var tipoSombreadoFromDb = await _context.TipoSombreados.Where(ts => ts.Id == muro.TipoSombreado.Id).FirstOrDefaultAsync();
                muroFromDb.TipoSombreado = tipoSombreadoFromDb;
            }

           

            muroFromDb.UpdatedAt = DateTime.Now;
            muroFromDb.Version = muroFromDb.Version + 1;
            muroFromDb.Active = true;
            muroFromDb.ModifiedBy = _currentUser.Id;
            muroFromDb.Azimut = muro.Bearing;
            muroFromDb.Distancia = muro.Distance;
            muroFromDb.Latitud = muro.Lat;
            muroFromDb.Longitud = muro.Lng;
            muroFromDb.Orientacion = muro.Orientation;
            muroFromDb.TipoMuro = muro.Tipo;
            muroFromDb.Vanos = muro.Vanos;
            muroFromDb.FachadaEste = FachadaEste(muroFromDb.Azimut, muroFromDb.Distancia, pisoFomDb.Altura);
            muroFromDb.FachadaNorte = FachadaNorte(muroFromDb.Azimut, muroFromDb.Distancia, pisoFomDb.Altura);
            muroFromDb.FachadaOeste = FachadaOeste(muroFromDb.Azimut, muroFromDb.Distancia, pisoFomDb.Altura);
            muroFromDb.FachadaSur = FachadaSur(muroFromDb.Azimut, muroFromDb.Distancia, pisoFomDb.Altura);
            await _context.SaveChangesAsync();

        }

        private double FachadaNorte(double azimut, double distancia, double altura)
        {
            if (azimut > -90 && azimut <= 90) {
                return Math.Abs(Math.Cos(ConvertToRadians(azimut)))*distancia* altura;
            }
            return 0;
        }
        private double FachadaEste(double azimut, double distancia, double altura)
        {
            if (azimut > 0)
            {
                return Math.Abs(Math.Sin(ConvertToRadians(azimut))) * distancia * altura;
            }
            return 0;
        }
        private double FachadaSur(double azimut, double distancia, double altura)
        {
            if (azimut <=-90 || azimut >90)
            {
                return Math.Abs(Math.Cos(ConvertToRadians(azimut))) * distancia * altura;
            }
            return 0;
        }
        private double FachadaOeste(double azimut, double distancia, double altura)
        {
            if (azimut <= 0)
            {
                return Math.Abs(Math.Sin(ConvertToRadians(azimut))) * distancia * altura;
            }
            return 0;
        }

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
