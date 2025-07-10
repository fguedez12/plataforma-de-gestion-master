using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.PisoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class PisoService : IPisoService
    {
        private readonly IPisoRepository _repoPiso;
        private readonly ITipoNivelPisoService _servicioTipoNivelPiso;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly Usuario _currentUser;
        private readonly ILogger _logger;


        public PisoService(IPisoRepository repoPiso, IMapper mapper, ITipoNivelPisoService servicioTipoNivelPiso,  
            UserManager<Usuario> userManager,  IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory,
            ApplicationDbContext context )
        {
            _repoPiso = repoPiso;
            _mapper = mapper;
            _servicioTipoNivelPiso = servicioTipoNivelPiso;
            _userManager = userManager;
            _context = context;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            _logger = loggerFactory.CreateLogger<PisoService>();
        }


        public async Task<IEnumerable<PisoModel>> GetAllAsync()
        {
            return await _repoPiso.All().ProjectTo<PisoModel>(_mapper.ConfigurationProvider).Where(a => a.Active).OrderBy(a => a.PisoNumero).ToListAsync();
        }

        public async Task<IEnumerable<PisoModel>> GetByDivisionId(long id)
        {
            var divisionFromDb = await _context.Divisiones.Where(d => d.Id == id).Include(d => d.Edificio).FirstOrDefaultAsync();


            var pisoListFromDb = await _repoPiso.Query()
                                .Where(p => p.EdificioId == divisionFromDb.Edificio.Id && p.Active)
                                .Include(m => m.Muros)
                                .ThenInclude(m=>m.TipoSombreado)
                                .Include(p => p.NumeroPiso)
                                .ToListAsync();

            foreach (var piso in pisoListFromDb)
            {
                piso.Muros = piso.Muros.Where(m => m.Active).ToList();
            }

            var pisoList = _mapper.Map<List<PisoModel>>(pisoListFromDb);


            //var pisoLis = await _repoPiso.Query().ProjectTo<PisoModel>(_mapper.ConfigurationProvider).Where(p => p.DivisionId == id && p.Active).OrderBy(p=>p.PisoNumero).ToListAsync();

            //foreach (var piso in pisoLis)
            //{
            //    piso.TipoNivel = _servicioTipoNivelPiso.Get(piso.NumeroPiso.Nivel);
            //}

            return pisoList;
        }

        public IEnumerable<PisoModel> All()
        {
            return _repoPiso
                .All()
                .OrderBy(o => o.NumeroPiso)
                .ProjectTo<PisoModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public async Task<List<PisoPasoUnoModel>> SavePiso(PisoForSaveModel model) 
        {
            long? id = null;
            if (model.InmuebleId == null)
            {
                id = model.DivisionId;
            }
            else
            {
                id = model.InmuebleId;
            }
            var divisionFromDb = await _context.Divisiones
                .Where(d => d.Id == id)
                .Include(d=>d.ListPisos)
                .ThenInclude(d => d.Areas)
                .Include(d => d.ListPisos)
                .ThenInclude(p => p.NumeroPiso)
                .Include(d => d.Edificio)
                .ThenInclude(e=>e.Pisos)
                .ThenInclude(p => p.NumeroPiso)
                .FirstOrDefaultAsync();

            var npisoFromDb = await _context.NumeroPisos.Where(n => n.Id == model.NumeroPisoId).FirstAsync();

            if (model.PisosIguales)
            {
                var numeroPisos = Math.Sqrt(Math.Pow(npisoFromDb.Numero, 2));
                if (model.EntrePiso)
                {
                    var nPiso = await _context.NumeroPisos.Where(n => n.Numero == 0).FirstOrDefaultAsync();
                    if (nPiso != null)
                    {
                        var pisoToSave = _mapper.Map<Piso>(model);
                        if (model.InmuebleId == null)
                        {
                            pisoToSave.EdificioId = divisionFromDb.EdificioId;
                            pisoToSave.DivisionId = null;

                        }
                        else
                        {
                            pisoToSave.DivisionId = divisionFromDb.Id;
                            pisoToSave.EdificioId = null;
                        }
                        pisoToSave.EdificioId = divisionFromDb.EdificioId;
                        pisoToSave.NumeroPisoId = nPiso.Id;
                        pisoToSave.CreatedAt = DateTime.Now;
                        pisoToSave.CreatedBy = _currentUser.Id;
                        pisoToSave.Active = true;
                        pisoToSave.Version = 1;
                        _repoPiso.Add(pisoToSave);
                    }

                }

                for (int i = 0; i < numeroPisos; i++)
                {
                    var num = 0;
                    if (npisoFromDb.Nivel == 1)
                    {
                        num = (i * -1) - 1;
                    }
                    else
                    {
                        num = i + 1;
                    }


                    var nPiso = await _context.NumeroPisos.Where(n => n.Numero == num).FirstAsync();

                    var pisoToSave = _mapper.Map<Piso>(model);
                    if (model.InmuebleId == null)
                    {
                        pisoToSave.EdificioId = divisionFromDb.EdificioId;
                        pisoToSave.DivisionId = null;

                    }
                    else
                    {
                        pisoToSave.DivisionId = divisionFromDb.Id;
                        pisoToSave.EdificioId = null;
                    }


                    pisoToSave.NumeroPisoId = nPiso.Id;
                    pisoToSave.CreatedAt = DateTime.Now;
                    pisoToSave.CreatedBy = _currentUser.Id;
                    pisoToSave.Active = true;
                    pisoToSave.Version = 1;
                    _repoPiso.Add(pisoToSave);
                }
            }
            else {
               
                var pisoToSave = _mapper.Map<Piso>(model);
                if (model.InmuebleId == null)
                {
                    pisoToSave.EdificioId = divisionFromDb.EdificioId;
                    pisoToSave.DivisionId = null;
                   
                }
                else
                {
                    pisoToSave.DivisionId = divisionFromDb.Id;
                    pisoToSave.EdificioId = null;
                }
               
                pisoToSave.NumeroPisoId = npisoFromDb.Id;
                pisoToSave.CreatedAt = DateTime.Now;
                pisoToSave.CreatedBy = _currentUser.Id;
                pisoToSave.Active = true;
                pisoToSave.Version = 1;
                _repoPiso.Add(pisoToSave);

            }


            
            await _repoPiso.SaveAll();

            var result = model.InmuebleId == null ? divisionFromDb.Edificio.Pisos. Where(p => p.Active).ToList() :  divisionFromDb.ListPisos.Where(p => p.Active).ToList();

            if (model.InmuebleId == null) {
                result = result.OrderBy(p => p.NumeroPiso.Numero).ToList();
            }
           
            return _mapper.Map<List<PisoPasoUnoModel>>(result); 
        }

        public async Task<List<PisoPasoUnoModel>> Update(long id, PisoModel pisoFromBody)
        {
            if (id != pisoFromBody.Id)
            {

                throw new Exception("El id no corresponde");
            }

            Piso pisoOriginal = _repoPiso.Query()
                .Where(p => p.Id == id)
                 .Include(p=>p.Edificio).ThenInclude(e=>e.Pisos)
                 .ThenInclude(p => p.NumeroPiso).FirstOrDefault();

            var pisoToEdit = _mapper.Map<Piso>(pisoOriginal);

            pisoToEdit.Altura = pisoFromBody.Altura;
            pisoToEdit.Superficie = pisoFromBody.Superficie;
            pisoToEdit.Active = true;
            pisoToEdit.UpdatedAt = DateTime.Now;
            pisoToEdit.Version = ++pisoOriginal.Version;
            pisoToEdit.CreatedAt = pisoOriginal.CreatedAt;
            pisoToEdit.ModifiedBy = _currentUser.Id;
            pisoToEdit.CreatedBy = pisoOriginal.CreatedBy;

            _repoPiso.Update(pisoToEdit);

            try
            {
                await _repoPiso.SaveAll();

                _logger.LogInformation($"piso [{pisoToEdit.Id}] actualizada por el usuario [{_currentUser.Id}]");

                var result = pisoOriginal.Edificio.Pisos.Where(p => p.Active).ToList();
                result = result.OrderBy(p => p.NumeroPiso.Numero).ToList();
                return _mapper.Map<List<PisoPasoUnoModel>>(result); ;

            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }

        public  async Task UpdateV2(long id, PisoModel pisoFromBody)
        {
            if (id != pisoFromBody.Id)
            {

                throw new Exception("El id no corresponde");
            }

            var pisoFromDb = await _context.Pisos.FirstOrDefaultAsync(x => x.Id == id);
            pisoFromDb.Altura = pisoFromBody.Altura;
            pisoFromDb.Superficie = pisoFromBody.Superficie;
            pisoFromDb.TipoUsoId = pisoFromBody.TipoUsoId;
            pisoFromDb.NroRol = pisoFromBody.NroRol;
            pisoFromDb.Active = true;
            pisoFromDb.UpdatedAt = DateTime.Now;
            pisoFromDb.Version = ++pisoFromDb.Version;
            pisoFromDb.CreatedAt = pisoFromDb.CreatedAt;
            pisoFromDb.ModifiedBy = _currentUser.Id;
            pisoFromDb.CreatedBy = pisoFromDb.CreatedBy;

            await _context.SaveChangesAsync();

        }

        //public async Task<long> SetFrontis(long id, int muroIndex)
        //{

        //    Piso pisoOriginal = _repoPiso.Query().Where(p => p.Id == id).FirstOrDefault();

        //    if (pisoOriginal == null)
        //    {
        //        return -1;
        //    }

        //    var pisoToEdit = _mapper.Map<Piso>(pisoOriginal);
        //    //pisoToEdit.FrontisIndex = muroIndex;
        //    pisoToEdit.Active = true;
        //    pisoToEdit.UpdatedAt = DateTime.Now;
        //    pisoToEdit.Version = ++pisoOriginal.Version;
        //    pisoToEdit.CreatedAt = pisoOriginal.CreatedAt;
        //    pisoToEdit.ModifiedBy = _currentUser.Id;
        //    pisoToEdit.CreatedBy = pisoOriginal.CreatedBy;

        //    _repoPiso.Update(pisoToEdit);

        //    try
        //    {
        //        await _repoPiso.SaveAll();

        //        _logger.LogInformation($"piso [{pisoToEdit.Id}] actualizada por el usuario [{_currentUser.Id}]");

        //    }
        //    catch (Exception ex)
        //    {
        //        return -2;
        //    }

        //    return pisoToEdit.Id;
        //}

        public async Task<List<PisoPasoUnoModel>> DeleteAsync(long id)
        {
            var piso = await _repoPiso.All().Where(p => p.Id == id).FirstOrDefaultAsync();
            if (piso == null)
            {
                throw new Exception("No existe el piso");
            }

            piso.Active = false;
            _repoPiso.Delete(piso);
            var result = _repoPiso.SaveChanges();

            _logger.LogInformation($"piso [{id}] desactivada por el usuario [{_currentUser.Id}]");

            var pisoListFromDb = await _context.Pisos.Where(p => p.EdificioId == piso.EdificioId && p.Active).Include(p=>p.NumeroPiso).ToListAsync();

            return _mapper.Map<List<PisoPasoUnoModel>>(pisoListFromDb);
        }

        public PisoModel Get(long id)
        {
            var piso = _repoPiso.Query().Where(p => p.Id == id).FirstOrDefault();

            return _mapper.Map<PisoModel>(piso);
        }

        public async Task AddUnidad(PisoUnidadRequest model)
        {
            var up = new UnidadPiso()
            {
                PisoId = model.PisoId,
                UnidadId = model.UnidadId
            };

            _context.UnidadesPisos.Add(up);
            await _context.SaveChangesAsync();

        }

        public async Task RemUnidad(PisoUnidadRequest model)
        {
            var up = await _context.UnidadesPisos
                .Where(upi => upi.PisoId == model.PisoId && upi.UnidadId == model.UnidadId)
                .FirstOrDefaultAsync();

            _context.UnidadesPisos.Remove(up);
            await _context.SaveChangesAsync();

        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public PisoModel Get(string id)
        {
            throw new NotImplementedException();
        }

        public string Insert(PisoModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(PisoModel model)
        {
            throw new NotImplementedException();
        }
    }
}
