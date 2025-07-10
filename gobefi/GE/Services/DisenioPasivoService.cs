using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CimientoModels;
using GobEfi.Web.Models.DisenioPasivoModels;
using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Models.MuroModels;
using GobEfi.Web.Models.PisoModels;
using GobEfi.Web.Models.PuertaModels;
using GobEfi.Web.Models.SueloModels;
using GobEfi.Web.Models.TechoModels;
using GobEfi.Web.Models.VentanaModels;
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
    public class DisenioPasivoService : IDisenioPasivoService
    {
        private readonly IEdificioRepository _repoEdificio;
        private readonly IDivisionRepository _repoDivision;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly Usuario _currentUser;
        private readonly ILogger _logger;

        public DisenioPasivoService(
            IEdificioRepository repoEdificio,
            IDivisionRepository repoDivision,
            IMapper mapper, UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory, ApplicationDbContext context )
        {
            _repoEdificio = repoEdificio;
            _repoDivision = repoDivision;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            _logger = loggerFactory.CreateLogger<PisoService>();

        }


        //public async Task<PasoUnoForSave> GetPasoUnoByDivisionId(long id)
        //{
        //    var divisionFromdb = await _repoDivision.Query().Where(d => d.Id == id).Include(d => d.Edificio).FirstOrDefaultAsync();
        //    var edificio = _mapper.Map<PasoUnoForSave>(divisionFromdb.Edificio);
        //    edificio.PisosIguales = divisionFromdb.PisosIguales;
            
        //    return edificio;
        //}

        public async Task<long> Update(long id, PasoUnoData pasoUno)
        {
            var divisionFDB = await _context.Divisiones.Where(d => d.Id == id).FirstOrDefaultAsync();

            var edificioOriginal = await _context.Edificios.Where(e => e.Id == divisionFDB.EdificioId).FirstOrDefaultAsync();

            //Edificio edificioOriginal = _repoDivision.Query().Include(d=>d.Edificio).Where(p => p.Id == id).Select(d =>d.Edificio).FirstOrDefault();


            if (edificioOriginal == null)
            {
                return -1;
            }

            //var edificioToEdit = _mapper.Map<Edificio>(edificioOriginal);

            edificioOriginal.TipoAgrupamientoId = pasoUno.TipoAgrupamientoId;
            edificioOriginal.EntornoId = pasoUno.EntornoId;
            edificioOriginal.Latitud = (double)pasoUno.Latitud;
            edificioOriginal.Longitud = (double)pasoUno.Longitud;
            edificioOriginal.Active = true;
            edificioOriginal.UpdatedAt = DateTime.Now;
            edificioOriginal.Version = ++edificioOriginal.Version;
            edificioOriginal.CreatedAt = edificioOriginal.CreatedAt;
            edificioOriginal.ModifiedBy = _currentUser.Id;
            edificioOriginal.CreatedBy = edificioOriginal.CreatedBy;

            //_repoEdificio.Update(edificioOriginal);
            _context.Edificios.Update(edificioOriginal);

            divisionFDB.DpSt1 = true;
            _context.Divisiones.Update(divisionFDB);

            try
            {
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Edificio [{edificioOriginal.Id}] actualizada por el usuario [{_currentUser.Id}]");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return edificioOriginal.Id;

        }

        public async Task PostMurosLv2(long id, DivisionModel model)
        {
            
            foreach (var pisoFromModel in model.Pisos)
            {
                foreach (var muroFromModel in pisoFromModel.Muros)
                {
                    if (muroFromModel.Id > 0 && muroFromModel.MaterialidadId > 0)
                    {
                        var pisoFromDb = await _context.Pisos.Where(p => p.Id == pisoFromModel.Id).Include(p => p.Muros).FirstOrDefaultAsync();

                        foreach (var muroFromDb in pisoFromDb.Muros)
                        {
                            if (muroFromDb.Id == muroFromModel.Id)
                            {
                                muroFromDb.UpdatedAt = DateTime.Now;
                                muroFromDb.Version = muroFromDb.Version + 1;
                                muroFromDb.Active = true;
                                muroFromDb.ModifiedBy = _currentUser.Id;
                                muroFromDb.MaterialidadId = muroFromModel.MaterialidadId;
                                muroFromDb.AislacionIntId = muroFromModel.AislacionIntId;
                                muroFromDb.AislacionExtId = muroFromModel.AislacionExtId;
                                muroFromDb.TipoSombreadoId = muroFromModel.TipoSombreadoId;
                                muroFromDb.Vanos = muroFromModel.Vanos;
                                muroFromDb.Largo = muroFromModel.Largo;
                                muroFromDb.Altura = muroFromModel.Altura;
                                muroFromDb.Superficie = muroFromModel.Superficie;
                                _context.Entry(muroFromDb).State = EntityState.Modified;
                            }
                        }
                    }
                }
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public async Task PostSuelosLv2(long id, DivisionModel model)
        {
            var queryDivision = _context.Divisiones.Where(d => d.Id == id).AsQueryable();

            var division = await queryDivision.Include(e=>e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p=>p.Suelos).FirstOrDefaultAsync();

            foreach (var piso in division.Edificio.Pisos.Where(p => p.Active))
            {

                foreach (var suelo in piso.Suelos)
                {
                    var elim = true;
                    foreach (var pisom in model.Pisos)
                    {
                        if (pisom.Id == piso.Id)
                        {
                            foreach (var suelom in pisom.Suelos)
                            {
                                if (suelom.Id == suelo.Id)
                                {
                                    elim = false;
                                }
                            }
                        }
                    }

                    if (elim)
                    {
                        suelo.UpdatedAt = DateTime.Now;
                        suelo.Version = suelo.Version + 1;
                        suelo.Active = false;
                        suelo.ModifiedBy = _currentUser.Id;
                        _context.Entry(suelo).State = EntityState.Modified;
                    }
                }
            }

            foreach (var pisoFromModel in model.Pisos)
            {
                
                foreach (var sueloFromModel in pisoFromModel.Suelos) {
                    if (sueloFromModel.Id == 0)
                    {
                        if (sueloFromModel.MaterialidadId > 0)
                        {
                            var newSuelo = new Suelo()
                            {
                                CreatedAt = DateTime.Now,
                                Version = 1,
                                Active = true,
                                CreatedBy = _currentUser.Id,
                                MaterialidadId = sueloFromModel.MaterialidadId,
                                AislacionId = sueloFromModel.AislacionId,
                                Superficie = sueloFromModel.Superficie,
                                PisoId = pisoFromModel.Id
                            };
                            _context.Suelos.Add(newSuelo);
                        }
                      
                    }
                    else {
                        var sueloFromDb = await _context.Suelos.Where(s => s.Id == sueloFromModel.Id).FirstOrDefaultAsync();
                        sueloFromDb.UpdatedAt = DateTime.Now;
                        sueloFromDb.Version = sueloFromDb.Version + 1;
                        sueloFromDb.Active = true;
                        sueloFromDb.ModifiedBy = _currentUser.Id;
                        sueloFromDb.MaterialidadId = sueloFromModel.MaterialidadId;
                        sueloFromDb.AislacionId = sueloFromModel.AislacionId;
                        sueloFromDb.Superficie = sueloFromModel.Superficie;
                        _context.Entry(sueloFromDb).State = EntityState.Modified;

                    }
                
                }

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task PostVentanasLv2(long id, DivisionModel model)
        {
            var queryDivision = _context.Divisiones.Where(d => d.Id == id).AsQueryable();

            var division = await queryDivision
                            .Include(e=>e.Edificio)
                            .ThenInclude(p => p.Pisos).ThenInclude(p => p.Muros).ThenInclude(m=>m.Ventanas).FirstOrDefaultAsync();

            foreach (var piso in division.Edificio.Pisos.Where(p => p.Active))
            {

                foreach (var muro in piso.Muros.Where(m=>m.Active))
                {
                    foreach (var ventana in muro.Ventanas.Where(v=>v.Active))
                    {
                        var elim = true;
                        foreach (var pisom in model.Pisos)
                        {
                            if (pisom.Id == piso.Id)
                            {
                                foreach (var murom in pisom.Muros)
                                {
                                    if (murom.Id == muro.Id)
                                    {
                                        foreach (var ventanam in murom.Ventanas)
                                        {
                                            if (ventanam.Id == ventana.Id)
                                            {
                                                elim = false;
                                            }
                                        }
                                    }
                                   
                                }
                            }
                        }

                        if (elim)
                        {
                            muro.UpdatedAt = DateTime.Now;
                            muro.Version = muro.Version + 1;
                            muro.Active = false;
                            muro.ModifiedBy = _currentUser.Id;
                            _context.Entry(muro).State = EntityState.Modified;
                        }
                    }
                   
                }
            }

            foreach (var pisoFromModel in model.Pisos)
            {

                foreach (var muroFromModel in pisoFromModel.Muros)
                {
                    foreach (var ventanaFromModel in muroFromModel.Ventanas)
                    {
                        if (ventanaFromModel.Id == 0)
                        {
                            if (ventanaFromModel.MaterialidadId > 0)
                            {
                                var newVentana = new Ventana()
                                {
                                    CreatedAt = DateTime.Now,
                                    Version = 1,
                                    Active = true,
                                    CreatedBy = _currentUser.Id,
                                    Ancho = ventanaFromModel.Ancho,
                                    Largo = ventanaFromModel.Largo,
                                    Numero = ventanaFromModel.Numero,
                                    Superficie = ventanaFromModel.Superficie,
                                    MaterialidadId = ventanaFromModel.MaterialidadId,
                                    TipoCierreId = ventanaFromModel.TipoCierreId,
                                    PosicionVentanaId = ventanaFromModel.PosicionVentanaId,
                                    TipoMarcoId = ventanaFromModel.TipoMarcoId,
                                    MuroId = muroFromModel.Id
                                };
                                _context.Ventanas.Add(newVentana);
                            }

                        }
                        else
                        {
                            var ventanaFromDb = await _context.Ventanas.Where(v => v.Id == ventanaFromModel.Id).FirstOrDefaultAsync();
                            ventanaFromDb.UpdatedAt = DateTime.Now;
                            ventanaFromDb.Version = ventanaFromDb.Version + 1;
                            ventanaFromDb.Active = true;
                            ventanaFromDb.ModifiedBy = _currentUser.Id;
                            ventanaFromDb.Ancho = ventanaFromModel.Ancho;
                            ventanaFromDb.Largo = ventanaFromModel.Largo;
                            ventanaFromDb.Numero = ventanaFromModel.Numero;
                            ventanaFromDb.MaterialidadId = ventanaFromModel.MaterialidadId;
                            ventanaFromDb.TipoCierreId = ventanaFromModel.TipoCierreId;
                            ventanaFromDb.PosicionVentanaId = ventanaFromModel.PosicionVentanaId;
                            ventanaFromDb.MuroId = ventanaFromModel.MuroId;
                            ventanaFromDb.Superficie = ventanaFromDb.Superficie;
                            _context.Entry(ventanaFromDb).State = EntityState.Modified;
                 
                        }
                    }

                   

                }

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task PostPasoTresV2(long id, PasoTresModel model)
        {
            var divisionDb =  await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == id);
            var edificioId = divisionDb.EdificioId;
            divisionDb.DpSt3 = true;
            divisionDb.DpSt4 = true;
            _context.Divisiones.Update(divisionDb);

            var pisoActive = await _context.Pisos.OrderBy(x=>x.Id).FirstOrDefaultAsync(x => x.EdificioId == edificioId && x.Active);


            //suelo

            var suelo = await _context.Suelos.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.PisoId == pisoActive.Id && x.Active);

            if (suelo == null)
            {
                var newSuelo = new Suelo()
                {
                    CreatedAt = DateTime.Now,
                    Version = 1,
                    Active = true,
                    CreatedBy = _currentUser.Id,
                    MaterialidadId = model.Piso.MaterialidadId,
                    AislacionId = model.Piso.AislacionId,
                    EspesorId = model.Piso.EspesorId,
                    PisoId = pisoActive.Id
                };
                _context.Suelos.Add(newSuelo);
            }
            else
            {
                suelo.UpdatedAt = DateTime.Now;
                suelo.Version = suelo.Version + 1;
                suelo.Active = true;
                suelo.ModifiedBy = _currentUser.Id;
                suelo.MaterialidadId = model.Piso.MaterialidadId;
                suelo.EspesorId = model.Piso.EspesorId;
                suelo.AislacionId = model.Piso.AislacionId;
                _context.Entry(suelo).State = EntityState.Modified;
            }

            //Muro

            var muro = await _context.Muros.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.PisoId == pisoActive.Id && x.Active);
                muro.UpdatedAt = DateTime.Now;
                muro.Version = muro.Version + 1;
                muro.ModifiedBy = _currentUser.Id;
                muro.AislacionIntId = model.Muro.AislacionIntId;
                muro.MaterialidadId = model.Muro.MaterialidadId;
                muro.EspesorId = model.Muro.EspesorId;
                _context.Entry(muro).State = EntityState.Modified;

            //ventanas
            var ventana = await _context.Ventanas.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.MuroId == muro.Id && x.Active);
            if (ventana==null)
            {
                var newVentana = new Ventana()
                {
                    CreatedAt = DateTime.Now,
                    Version = 1,
                    Active = true,
                    CreatedBy = _currentUser.Id,
                    MaterialidadId = model.Ventanas.MaterialidadId,
                    TipoCierreId = model.Ventanas.TipoCierreId,
                    TipoMarcoId = model.Ventanas.TipoMarcoId,
                    Superficie = model.Ventanas.Superficie,
                    MuroId = muro.Id
                };
                _context.Ventanas.Add(newVentana);
            }
            else
            {

                ventana.UpdatedAt = DateTime.Now;
                ventana.Version = ventana.Version + 1;
                ventana.Active = true;
                ventana.ModifiedBy = _currentUser.Id;
                ventana.MaterialidadId = model.Ventanas.MaterialidadId;
                ventana.TipoCierreId = model.Ventanas.TipoCierreId;
                ventana.TipoMarcoId = model.Ventanas.TipoMarcoId;
                ventana.Superficie = model.Ventanas.Superficie;
                _context.Entry(ventana).State = EntityState.Modified;
            }

            //puerta
            var puerta = await _context.Puertas.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.MuroId == muro.Id && x.Active);
            if (puerta==null)
            {

                var newPuerta = new Puerta()
                {
                    CreatedAt = DateTime.Now,
                    Version = 1,
                    Active = true,
                    CreatedBy = _currentUser.Id,
                    MaterialidadId = model.Puertas.MaterialidadId,
                    TipoMarcoId = model.Puertas.TipoMarcoId,
                    Superficie = model.Puertas.Superficie,
                    MuroId = muro.Id
                };
                _context.Puertas.Add(newPuerta); 
            }
            else
            {

                puerta.UpdatedAt = DateTime.Now;
                puerta.Version = puerta.Version + 1;
                puerta.Active = true;
                puerta.ModifiedBy = _currentUser.Id;
                puerta.MaterialidadId = model.Puertas.MaterialidadId;
                puerta.TipoMarcoId = model.Puertas.TipoMarcoId;
                puerta.Superficie = model.Puertas.Superficie;
                _context.Entry(puerta).State = EntityState.Modified;
            }

            // techo

            var techo = await _context.Techos.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.EdificioId == edificioId);
            if (techo == null)
            {
                var newTecho = new Techo()
                {
                    CreatedAt = DateTime.Now,
                    Version = 1,
                    Active = true,
                    CreatedBy = _currentUser.Id,
                    MaterialidadId = model.Techo.MaterialidadId,
                    AislacionId = model.Techo.AislacionId,
                    EspesorId = model.Techo.EspesorId,
                    EdificioId = edificioId
                };
                _context.Techos.Add(newTecho);
            }
            else
            {
                techo.UpdatedAt = DateTime.Now;
                techo.Version = techo.Version + 1;
                techo.Active = true;
                techo.ModifiedBy = _currentUser.Id;
                techo.MaterialidadId = model.Techo.MaterialidadId;
                techo.AislacionId = model.Techo.AislacionId;
                techo.EspesorId = model.Techo.EspesorId;
                _context.Entry(techo).State = EntityState.Modified;

            }

            //cimiento

            var cimiento = await _context.Cimientos.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.EdificioId == edificioId);
            if (cimiento == null)
            {
                var newCimiento = new Cimiento()
                {
                    CreatedAt = DateTime.Now,
                    Version = 1,
                    Active = true,
                    CreatedBy = _currentUser.Id,
                    MaterialidadId = model.Cimiento.MaterialidadId,
                    EdificioId = edificioId
                };

                _context.Cimientos.Add(newCimiento);
            }
            else
            {
                cimiento.UpdatedAt = DateTime.Now;
                cimiento.Version = cimiento.Version + 1;
                cimiento.Active = true;
                cimiento.ModifiedBy = _currentUser.Id;
                cimiento.MaterialidadId = model.Cimiento.MaterialidadId;
                _context.Entry(cimiento).State = EntityState.Modified;

            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public async Task PostPasoTres(long id, PasoTresModel model) 
        {
            var queryDivision = _context.Divisiones.Where(d => d.Id == id).AsQueryable();

            var queryEdificio = queryDivision.Include(e => e.Edificio).Select(e=>e.Edificio).AsQueryable();

            var division = await queryDivision
                            .Include(e=>e.Edificio).ThenInclude(p=>p.Pisos).ThenInclude(p=>p.Muros).ThenInclude(m=>m.Ventanas)
                            .Include(e => e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p => p.Muros).ThenInclude(m => m.Puertas)
                            .Include(e => e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p => p.Suelos)
                            .FirstAsync();
            division.DpSt3 = true;
            division.DpSt4 = true;
            _context.Divisiones.Update(division);


            var pisos = division.Edificio.Pisos.Where(p=>p.Active);

            var edificio = await queryEdificio.Include(e=>e.Cimiento).FirstOrDefaultAsync();

            var techo = await queryEdificio.Include(e => e.Techo).Select(e=>e.Techo).FirstOrDefaultAsync();


            // techo

           
                if (techo == null)
                {
                    var newTecho = new Techo()
                    {
                        CreatedAt = DateTime.Now,
                        Version = 1,
                        Active = true,
                        CreatedBy = _currentUser.Id,
                        MaterialidadId = model.Techo.MaterialidadId,
                        AislacionId = model.Techo.AislacionId,
                        EspesorId = model.Techo.EspesorId,
                        EdificioId = edificio.Id
                    };
                    _context.Techos.Add(newTecho);
                }
                else
                {
                    techo.UpdatedAt = DateTime.Now;
                    techo.Version = techo.Version + 1;
                    techo.Active = true;
                    techo.ModifiedBy = _currentUser.Id;
                    techo.MaterialidadId = model.Techo.MaterialidadId;
                    techo.AislacionId = model.Techo.AislacionId;
                    techo.EspesorId = model.Techo.EspesorId;
                    _context.Entry(techo).State = EntityState.Modified;

                }
            

            

            //Update muros

            foreach (var piso in pisos)
            {
                foreach (var muro in piso.Muros)
                {
                    if (muro.Active)
                    {
                        muro.UpdatedAt = DateTime.Now;
                        muro.Version = muro.Version + 1;
                        muro.ModifiedBy = _currentUser.Id;
                        muro.AislacionIntId = model.Muro.AislacionIntId;
                        muro.MaterialidadId = model.Muro.MaterialidadId;
                        muro.EspesorId = model.Muro.EspesorId;
                        _context.Entry(muro).State = EntityState.Modified;

                        if (muro.Ventanas.Count > 0)
                        {
                            foreach (var ventana in muro.Ventanas)
                            {
                                ventana.UpdatedAt = DateTime.Now;
                                ventana.Version = ventana.Version + 1;
                                ventana.Active = true;
                                ventana.ModifiedBy = _currentUser.Id;
                                ventana.MaterialidadId = model.Ventanas.MaterialidadId;
                                ventana.TipoCierreId = model.Ventanas.TipoCierreId;
                                ventana.TipoMarcoId = model.Ventanas.TipoMarcoId;
                                ventana.Superficie = model.Ventanas.Superficie;
                                _context.Entry(ventana).State = EntityState.Modified;
                            }
                        }
                        else {
                            
                                var newVentana = new Ventana()
                                {
                                    CreatedAt = DateTime.Now,
                                    Version = 1,
                                    Active = true,
                                    CreatedBy = _currentUser.Id,
                                    MaterialidadId = model.Ventanas.MaterialidadId,
                                    TipoCierreId = model.Ventanas.TipoCierreId,
                                    TipoMarcoId = model.Ventanas.TipoMarcoId,
                                    Superficie = model.Ventanas.Superficie,
                                    MuroId = muro.Id
                                };
                                _context.Ventanas.Add(newVentana);
                            
                           

                        }
                        if (muro.Puertas.Count > 0)
                        {
                            foreach (var puerta in muro.Puertas)
                            {
                                puerta.UpdatedAt = DateTime.Now;
                                puerta.Version = puerta.Version + 1;
                                puerta.Active = true;
                                puerta.ModifiedBy = _currentUser.Id;
                                puerta.MaterialidadId = model.Puertas.MaterialidadId;
                                puerta.TipoMarcoId = model.Puertas.TipoMarcoId;
                                puerta.Superficie = model.Puertas.Superficie;
                                _context.Entry(puerta).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            
                                var newPuerta = new Puerta()
                                {
                                    CreatedAt = DateTime.Now,
                                    Version = 1,
                                    Active = true,
                                    CreatedBy = _currentUser.Id,
                                    MaterialidadId = model.Puertas.MaterialidadId,
                                    TipoMarcoId = model.Puertas.TipoMarcoId,
                                    Superficie = model.Puertas.Superficie,
                                    MuroId = muro.Id
                                };
                                _context.Puertas.Add(newPuerta);
                            
                           

                        }


                    }

                }
            }
            //Suelos
            foreach (var piso in pisos)
            {
               
                    if (piso.Active)
                    {
                        if (piso.Suelos == null || piso.Suelos.Count == 0)
                        {
                            var newSuelo = new Suelo()
                            {
                                CreatedAt = DateTime.Now,
                                Version = 1,
                                Active = true,
                                CreatedBy = _currentUser.Id,
                                MaterialidadId = model.Piso.MaterialidadId,
                                AislacionId = model.Piso.AislacionId,
                                EspesorId = model.Piso.EspesorId,
                                PisoId = piso.Id
                            };
                            _context.Suelos.Add(newSuelo);
                        }
                        else
                        {
                            foreach (var suelo in piso.Suelos)
                            {
                                suelo.UpdatedAt = DateTime.Now;
                                suelo.Version = piso.Version + 1;
                                suelo.Active = true;
                                suelo.ModifiedBy = _currentUser.Id;
                                suelo.MaterialidadId = model.Piso.MaterialidadId;
                                suelo.EspesorId = model.Piso.EspesorId;
                                suelo.AislacionId = model.Piso.AislacionId;
                                _context.Entry(suelo).State = EntityState.Modified;
                            }
                        }
                    }
                

                
            }
            //Cimiento

                if (edificio.Cimiento == null)
                {
                    var newCimiento = new Cimiento()
                    {
                        CreatedAt = DateTime.Now,
                        Version = 1,
                        Active = true,
                        CreatedBy = _currentUser.Id,
                        MaterialidadId = model.Cimiento.MaterialidadId,
                        EdificioId = edificio.Id
                    };

                    _context.Cimientos.Add(newCimiento);
                }
                else
                {
                    edificio.Cimiento.UpdatedAt = DateTime.Now;
                    edificio.Cimiento.Version = edificio.Cimiento.Version + 1;
                    edificio.Cimiento.Active = true;
                    edificio.Cimiento.ModifiedBy = _currentUser.Id;
                    edificio.Cimiento.MaterialidadId = model.Cimiento.MaterialidadId;
                    _context.Entry(edificio.Cimiento).State = EntityState.Modified;

                }
            



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }

        public async Task<DivisionModel> GetDivision(long id)
        {
            var queryDivision = _context.Divisiones.Where(d => d.Id == id)
                                .AsQueryable();
                    
            var queryEdificio = queryDivision
                                .Include(d => d.Edificio)
                                .Select(d => d.Edificio)
                                .AsQueryable();

            var edificioFromDb = await queryEdificio
                                    .Include(e => e.Entorno)
                                    .Include(e => e.TipoAgrupamiento)
                                    .Include(e => e.Techo)
                                    .Include(e => e.Cimiento)
                                    .FirstOrDefaultAsync();
            var response = new DivisionModel();

            var dividionFromDb = await queryDivision

                                    .Include(e => e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p => p.NumeroPiso)
                                    .Include(e => e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p => p.Suelos)
                                    .Include(e => e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p => p.Muros).ThenInclude(m => m.TipoSombreado)
                                    .Include(e => e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p => p.Muros).ThenInclude(m => m.Materialidad)
                                    .Include(e => e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p => p.Muros).ThenInclude(m => m.Ventanas)
                                    .Include(e => e.Edificio).ThenInclude(p => p.Pisos).ThenInclude(p => p.Muros).ThenInclude(m => m.Puertas)
                                    .FirstOrDefaultAsync();

            var pisosFromDb = dividionFromDb.Edificio.Pisos.Where(p => p.Active).OrderBy(p=>p.NumeroPiso.Numero).ToList();
            foreach (var piso in dividionFromDb.Edificio.Pisos)
            {
                var murosFromDb = piso.Muros.Where(m => m.Active).ToList();
                piso.Muros = murosFromDb;
                var suelosFromDb = piso.Suelos.Where(s => s.Active).ToList();
                piso.Suelos = suelosFromDb;
            }

            var archivos = await _context.ArchivosDp.Where(a => a.DivisionId == id && a.Active).ToListAsync();

            response.PisosIguales = dividionFromDb.PisosIguales;
            response.Edificio = _mapper.Map<EdificioDPModel>(edificioFromDb);
            response.Pisos = _mapper.Map<List<PisoModel>>(pisosFromDb);
            response.NivelPaso3 = dividionFromDb.NivelPaso3;
            response.Archivos = _mapper.Map<List<ArchivoDpModel>>(archivos);

            return response;
        }

        public async Task<PasoUnoData> GetPasoUnoData(long id) 
        {
            var divisionFromDb = await _context.Divisiones.Where(d => d.Id == id).FirstOrDefaultAsync();


            var edificioFromDb = await _context.Edificios
                                    .Where(e => e.Id == divisionFromDb.EdificioId)
                                    .FirstOrDefaultAsync();
            
            var response = _mapper.Map<PasoUnoData>(edificioFromDb);
            response.Pisos = await GetPisosByEdificioId(edificioFromDb.Id);
            response.DpSt1 = divisionFromDb.DpSt1;
            response.DpSt2 = divisionFromDb.DpSt2;
            response.DpSt3 = divisionFromDb.DpSt3;
            response.DpSt4 = divisionFromDb.DpSt4;
            return response;
        }

        public async Task<PasoCuatroData> GetPasoCuatroData(long id)
        {
            var divisionFromDb = await _context.Divisiones
                                    .Where(d => d.Id == id)
                                    .Include(d=>d.ArchivosDp)
                                    .FirstOrDefaultAsync();
            var response = new PasoCuatroData() { 
                Archivos = _mapper.Map<List<ArchivoPasoCuatroModel>>(divisionFromDb.ArchivosDp.Where(a=>a.Active).ToList())
            };

            return response;
        }

        public async Task<PasoDosData> GetPasoDosData(long id)
        {
            var divisionFromDb = await _context.Divisiones.Where(d => d.Id == id).FirstOrDefaultAsync();

            var edificioFromDb = await _context.Divisiones
                                    .Where(d => d.Id == id)
                                    .Select(d => d.Edificio)
                                    .Include(e => e.Pisos)
                                    .ThenInclude(p=>p.Muros)
                                    .Include(e => e.Pisos)
                                    .ThenInclude(p => p.NumeroPiso)
                                    .FirstOrDefaultAsync();

            var listPisos = edificioFromDb.Pisos.Where(p=>p.Active);
            foreach (var piso in listPisos)
            {
                piso.Muros = piso.Muros.Where(m => m.Active).ToList();
            }
            var response = new PasoDosData() {
                FrontisId = edificioFromDb.FrontisId,
                PisosIguales = divisionFromDb.PisosIguales,
                Pisos = _mapper.Map<List<PisoPasoDosModel>>(listPisos)
            };
            return response;
        }


        public async Task<PasoTresModel> GetPasoTresDataV2(long id)
        {
            var rPiso = new PisoPasoTresModel();
            var rMuro = new MuroPasoTresModel();
            var rVentanas = new VentanasPasoTres();
            var rPuerta = new PuertasPasoTres();
            var rTecho = new TechoPasoTres();
            var rCimiento = new CimientoPasoTres();

            var edificioId = await _context.Divisiones.Where(d => d.Id == id).Select(d=>d.Edificio.Id).FirstOrDefaultAsync();

            var techo = await _context.Techos.FirstOrDefaultAsync(x => x.EdificioId == edificioId);

            var cimiento = await _context.Cimientos.FirstOrDefaultAsync(x => x.EdificioId == edificioId) ;

            var pisoActive = await _context.Pisos.Where(p=>p.EdificioId == edificioId && p.Active).OrderBy(p => p.Id).FirstOrDefaultAsync();

            if (pisoActive != null)
            {
                var suelo = await _context.Suelos.OrderBy(x=>x.Id).FirstOrDefaultAsync(x=>x.PisoId == pisoActive.Id && x.Active);
                if (suelo != null)
                {
                    rPiso.AislacionId = suelo.AislacionId;
                    rPiso.EspesorId = suelo.EspesorId;
                    rPiso.MaterialidadId = suelo.MaterialidadId;
                }

                var muro = await _context.Muros.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.PisoId == pisoActive.Id && x.Active);
                if (muro != null)
                {
                    rMuro.AislacionIntId = muro.AislacionIntId;
                    rMuro.EspesorId = muro.EspesorId;
                    rMuro.MaterialidadId = muro.MaterialidadId;

                    var ventana = await _context.Ventanas.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.MuroId == muro.Id && x.Active); 
                    if (ventana != null)
                    {
                        rVentanas.MaterialidadId = ventana.MaterialidadId;
                        rVentanas.Superficie = ventana.Superficie;
                        rVentanas.TipoCierreId = ventana.TipoCierreId;
                        rVentanas.TipoMarcoId = ventana.TipoMarcoId;
                    }
                    var puerta = await _context.Puertas.OrderBy(x => x.Id).FirstOrDefaultAsync(x => x.MuroId == muro.Id && x.Active);
                    if (puerta != null)
                    {
                        rPuerta.MaterialidadId = puerta.MaterialidadId;
                        rPuerta.Superficie = puerta.Superficie;
                        rPuerta.TipoMarcoId = puerta.TipoMarcoId;
                    }
                }
            }

            
            if (techo != null)
            {
                rTecho.AislacionId = techo.AislacionId;
                rTecho.EspesorId = techo.EspesorId;
                rTecho.MaterialidadId = techo.MaterialidadId;
            }

           
            if (cimiento != null)
            {
                rCimiento.MaterialidadId = cimiento.MaterialidadId;
            }



            var response = new PasoTresModel() { };
            response.Piso = rPiso;
            response.Muro = rMuro;
            response.Ventanas = rVentanas;
            response.Puertas = rPuerta;
            response.Techo = rTecho;
            response.Cimiento = rCimiento;

            return response;
        }
            
        

        public async Task<PasoTresModel> GetPasoTresData(long id)
        {
            var rPiso = new PisoPasoTresModel();
            var rMuro = new MuroPasoTresModel();
            var rVentanas = new VentanasPasoTres();
            var rPuerta = new PuertasPasoTres();
            var rTecho = new TechoPasoTres();
            var rCimiento = new CimientoPasoTres();

            var divisionFromDb = await _context.Divisiones.Where(d => d.Id == id).FirstOrDefaultAsync();


            var edificioFromDb = await _context.Edificios
                                    .Include(e=>e.Pisos)
                                    .ThenInclude(p=>p.Suelos)
                                    .Include(e => e.Pisos)
                                    .ThenInclude(p=>p.Muros)
                                    .ThenInclude(m =>m.Ventanas)
                                    .Include(e => e.Pisos)
                                    .ThenInclude(p => p.Muros)
                                    .ThenInclude(m => m.Puertas)
                                    .Include(e=>e.Techo)
                                    .Include(e=>e.Cimiento)
                                    .Where(e => e.Id == divisionFromDb.EdificioId)
                                    .FirstOrDefaultAsync();

            var pisosActive = edificioFromDb.Pisos.Where(p=>p.Active).OrderBy(p=>p.Id).ToList();
            var piso = pisosActive.FirstOrDefault();

            if (piso != null) {
                var suelo = piso.Suelos.FirstOrDefault();
                if (suelo != null) {
                    rPiso.AislacionId = suelo.AislacionId;
                    rPiso.EspesorId = suelo.EspesorId;
                    rPiso.MaterialidadId = suelo.MaterialidadId;
                }

                var muro = piso.Muros.FirstOrDefault();
                if (muro != null) {
                    rMuro.AislacionIntId = muro.AislacionIntId;
                    rMuro.EspesorId = muro.EspesorId;
                    rMuro.MaterialidadId = muro.MaterialidadId;

                    var ventana = muro.Ventanas.FirstOrDefault();
                    if (ventana != null) {
                        rVentanas.MaterialidadId = ventana.MaterialidadId;
                        rVentanas.Superficie = ventana.Superficie;
                        rVentanas.TipoCierreId = ventana.TipoCierreId;
                        rVentanas.TipoMarcoId = ventana.TipoMarcoId;
                    }
                    var puerta = muro.Puertas.FirstOrDefault();
                    if (puerta != null)
                    {
                        rPuerta.MaterialidadId = puerta.MaterialidadId;
                        rPuerta.Superficie = puerta.Superficie;
                        rPuerta.TipoMarcoId = puerta.TipoMarcoId;
                    }
                }
            }

            var techo = edificioFromDb.Techo;
            if (techo != null) {
                rTecho.AislacionId = techo.AislacionId;
                rTecho.EspesorId = techo.EspesorId;
                rTecho.MaterialidadId = techo.MaterialidadId;
            }

            var cimiento = edificioFromDb.Cimiento;
            if (cimiento != null) {
                rCimiento.MaterialidadId = cimiento.MaterialidadId;
            }
  
           

            var response = new PasoTresModel() { };
            response.Piso = rPiso;
            response.Muro = rMuro;
            response.Ventanas = rVentanas;
            response.Puertas = rPuerta;
            response.Techo = rTecho;
            response.Cimiento = rCimiento;

            return response;
        }

        private async  Task<List<PisoPasoUnoModel>> GetPisosByEdificioId(long id)
        {
            var pisosListFromDb = await _context.Pisos.Include(p => p.NumeroPiso).Where(p => p.EdificioId == id && p.Active).ToListAsync();

            var pisosList = _mapper.Map<List<PisoPasoUnoModel>>(pisosListFromDb);
            return pisosList;
        }

        public async Task LevelPaso3(long id)
        {
            var divisionFromDb = await _context.Divisiones.Where(d => d.Id == id).FirstOrDefaultAsync();
            var actualLevel = divisionFromDb.NivelPaso3;
            if (actualLevel == 0 || actualLevel == 1) {
                divisionFromDb.NivelPaso3 = 2;
            }
            else {
                divisionFromDb.NivelPaso3 = 1;
            }

            await _context.SaveChangesAsync();
                               
        }

        public async Task<ArchivoDpModel> PostFile(ArchivoDpModel model) {
            try
            {
                var newFile = new ArchivoDp() {
                    Active = true,
                    CreatedAt = model.Fecha,
                    CreatedBy = _currentUser.Id,
                    DivisionId = model.DivisionId,
                    FileUrl = model.FileUrl,
                    NombreArchivo = model.NombreArchivo,
                    Peso = model.Peso,
                    Seccion = model.Seccion,
                    Version = 1
                };

                _context.ArchivosDp.Add(newFile);
                await _context.SaveChangesAsync();
                model.Id = newFile.Id;
                return (model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task DeleteFile(long id)
        {
            try
            {
                var fileFormDb = await _context.ArchivosDp.Where(a => a.Id == id).FirstOrDefaultAsync();
                if (fileFormDb != null) {
                    fileFormDb.UpdatedAt = DateTime.Now;
                    fileFormDb.Version = fileFormDb.Version + 1;
                    fileFormDb.Active = false;
                    fileFormDb.ModifiedBy = _currentUser.Id;
                    _context.Entry(fileFormDb).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task UpdateFile(ArchivoDpModel model)
        {
            try
            {
                var fileFormDb = await _context.ArchivosDp.Where(a => a.Id == model.Id).FirstOrDefaultAsync();
                if (fileFormDb != null)
                {
                    fileFormDb.UpdatedAt = DateTime.Now;
                    fileFormDb.Version = fileFormDb.Version + 1;
                    fileFormDb.ModifiedBy = _currentUser.Id;
                    fileFormDb.Nombre = model.Nombre;
                    fileFormDb.Descripcion = model.Descripcion;
                    _context.Entry(fileFormDb).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ArchivoDpModel> GetFileById(long id) {
            var fileFromDb = await _context.ArchivosDp.Where(a => a.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ArchivoDpModel>(fileFromDb);
        }

        public async Task<long?> setFrontis(long id, long muroId) {

            var edificioFromDb = await _context.Divisiones
                                    .Where(d => d.Id == id)
                                    .Include(d => d.Edificio)
                                    .Select(d => d.Edificio).FirstOrDefaultAsync();

            edificioFromDb.FrontisId = muroId;
            edificioFromDb.ModifiedBy = _currentUser.Id;
            edificioFromDb.UpdatedAt = DateTime.Now;
            edificioFromDb.Version++;
            _context.Entry(edificioFromDb).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

                return edificioFromDb.FrontisId;

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task PasoDosComplete(long id)
        {
            var divisionFDB = await _context.Divisiones.Where(d => d.Id == id).FirstOrDefaultAsync();
            divisionFDB.DpSt2 = true;
            _context.Divisiones.Update(divisionFDB);
            await _context.SaveChangesAsync();
        }

    }

    


}
