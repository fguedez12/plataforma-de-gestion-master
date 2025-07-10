using GobEfi.Services.Models;
using GobEfi.Services.Models.MedidoresModels;
using GobEfi.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Services
{

    public class MedidorService : IMedidorService
    {
        private readonly DbContext context;
        private readonly string CREATOR_NAME = "API";

        public MedidorService(DbContext context)
        {
            this.context = context;
        }

        public async Task<MedidorInteligente> Create(MedidorModel medidor)
        {
            var medidorFromDb = await context.MedidoresInteligentes.Where(x => 
                x.ChileMedidoId == medidor.ChileMedidoId).FirstOrDefaultAsync();

            if (medidorFromDb is null)
            {
                var nuevoMedidor = new MedidorInteligente()
                {
                    Active = true,
                    ChileMedidoId = Convert.ToInt64(medidor.ChileMedidoId),
                    CreatedAt = DateTime.Now,
                    CreatedBy = CREATOR_NAME,
                    Version = 1
                };

                if (medidor.TipoEntidad == Entidades.Edificio.ToString())
                {
                    nuevoMedidor.MedidoresInteligentesEdificios = new List<MedidorInteligenteEdificio>();
                    foreach (var id in medidor.EntidadIds)
                    {
                        var EdificioFromDb = await context.Edificios.Where(x => x.Id == id).FirstOrDefaultAsync();

                        if (EdificioFromDb is null)
                        {
                            throw new Exception($"El edificio con id:{id}  no existe en la base de datos");
                        }

                        var nuevoEdificio = new MedidorInteligenteEdificio() { 
                            Active = true,
                            CreatedAt = DateTime.Now,
                            CreatedBy = CREATOR_NAME,
                            EdificioId = id,
                            Version = 1
                        };
                        nuevoMedidor.MedidoresInteligentesEdificios.Add(nuevoEdificio);
                    }
                }

                if (medidor.TipoEntidad == Entidades.Servicio.ToString())
                {
                  
                    nuevoMedidor.MedidoresIntelligentesServicios = new List<MedidorInteligenteServicio>();
                    foreach (var id in medidor.EntidadIds)
                    {
                        var ServicioFromDb = await context.Servicios.Where(x => x.Id == id).FirstOrDefaultAsync();

                        if (ServicioFromDb is null)
                        {
                            throw new Exception($"El servicio con id:{id}  no existe en la base de datos");
                        }
                        var nuevoServicios = new MedidorInteligenteServicio()
                        {
                            Active = true,
                            CreatedAt = DateTime.Now,
                            CreatedBy = CREATOR_NAME,
                            ServicioId = id,
                            Version = 1
                        };
                        nuevoMedidor.MedidoresIntelligentesServicios.Add(nuevoServicios);
                    }
                }
                if (medidor.TipoEntidad == Entidades.Division.ToString())
                {
                    nuevoMedidor.MedidorInteligenteDivisiones = new List<MedidorInteligenteDivision>();
                    foreach (var id in medidor.EntidadIds)
                    {
                        var DivisionFromDb = await context.Divisiones.Where(x => x.Id == id).FirstOrDefaultAsync();

                        if (DivisionFromDb is null)
                        {
                            throw new Exception($"La División con id:{id}  no existe en la base de datos");
                        }
                        var nuevaDivision = new MedidorInteligenteDivision()
                        {
                            Active = true,
                            CreatedAt = DateTime.Now,
                            CreatedBy = CREATOR_NAME,
                            DivisionId = id,
                            Version = 1
                        };
                        nuevoMedidor.MedidorInteligenteDivisiones.Add(nuevaDivision);
                    }
                }


                context.MedidoresInteligentes.Add(nuevoMedidor);
                await context.SaveChangesAsync();
                return nuevoMedidor;
            }
            else
            {
                medidorFromDb.ModifiedBy = CREATOR_NAME;
                medidorFromDb.UpdatedAt = DateTime.Now;
                medidorFromDb.Version = medidorFromDb.Version + 1;

                await context.Database.ExecuteSqlInterpolatedAsync(
                    $"UPDATE MedidorInteligenteEdificios SET Active = 0 WHERE MedidorInteligenteId = {medidorFromDb.Id};UPDATE MedidorInteligenteServicios SET Active = 0 WHERE MedidorInteligenteId = {medidorFromDb.Id};UPDATE MedidorInteligenteDivisiones SET Active = 0 WHERE MedidorInteligenteId = {medidorFromDb.Id}");

                if (medidor.TipoEntidad == Entidades.Edificio.ToString())
                {
                    medidorFromDb.MedidoresInteligentesEdificios = new List<MedidorInteligenteEdificio>();
                    
                    foreach (var id in medidor.EntidadIds)
                    {
                        var EdificioFromDb = await context.Edificios.Where(x => x.Id == id).FirstOrDefaultAsync();

                        if (EdificioFromDb is null)
                        {
                            throw new Exception($"El edificio con id:{id}  no existe en la base de datos");
                        }

                        var nuevoEdificio = new MedidorInteligenteEdificio()
                        {
                            Active = true,
                            CreatedAt = DateTime.Now,
                            CreatedBy = CREATOR_NAME,
                            EdificioId = id,
                            Version = 1
                        };
                        medidorFromDb.MedidoresInteligentesEdificios.Add(nuevoEdificio);
                    }
                }

                if (medidor.TipoEntidad == Entidades.Servicio.ToString())
                {
                    medidorFromDb.MedidoresIntelligentesServicios = new List<MedidorInteligenteServicio>();
                    foreach (var id in medidor.EntidadIds)
                    {
                        var ServicioFromDb = await context.Servicios.Where(x => x.Id == id).FirstOrDefaultAsync();

                        if (ServicioFromDb is null)
                        {
                            throw new Exception($"El servicio con id:{id}  no existe en la base de datos");
                        }
                        var nuevoServicios = new MedidorInteligenteServicio()
                        {
                            Active = true,
                            CreatedAt = DateTime.Now,
                            CreatedBy = CREATOR_NAME,
                            ServicioId = id,
                            Version = 1
                        };
                        medidorFromDb.MedidoresIntelligentesServicios.Add(nuevoServicios);
                    }
                }
                if (medidor.TipoEntidad == Entidades.Division.ToString())
                {
                    medidorFromDb.MedidorInteligenteDivisiones = new List<MedidorInteligenteDivision>();
                    foreach (var id in medidor.EntidadIds)
                    {
                        var DivisionFromDb = await context.Divisiones.Where(x => x.Id == id).FirstOrDefaultAsync();

                        if (DivisionFromDb is null)
                        {
                            throw new Exception($"La División con id:{id}  no existe en la base de datos");
                        }
                        var nuevaDivision = new MedidorInteligenteDivision()
                        {
                            Active = true,
                            CreatedAt = DateTime.Now,
                            CreatedBy = CREATOR_NAME,
                            DivisionId = id,
                            Version = 1
                        };
                        medidorFromDb.MedidorInteligenteDivisiones.Add(nuevaDivision);
                    }
                }
                await context.SaveChangesAsync();
                return medidorFromDb;
            }
        }

        public async Task<List<UnidadModel>> GetByUnidades(long[] ids)
        {
            var listUnidades = new List<UnidadModel>();
            foreach (var id in ids)
            {
                var newUnidad = new UnidadModel();

                var unidadFromDb = await context.Divisiones.Where(d => d.Id == id)
                    .Include(u=>u.MedidorDivision).ThenInclude(md=>md.Medidor)
                    .FirstOrDefaultAsync();
                if (unidadFromDb != null)
                {
                    newUnidad.Id = unidadFromDb.Id;
                    newUnidad.Nombre = unidadFromDb.Nombre;
                    newUnidad.Medidores = new List<MedidoresModel>();
                    foreach (var medidor in unidadFromDb.MedidorDivision)
                    {
                        if (medidor.Active && medidor.Medidor.Active)
                        {
                            var newMedidor = new MedidoresModel()
                            {
                                Id = medidor.Id,
                                Numero = medidor.Medidor.Numero
                            };
                            newUnidad.Medidores.Add(newMedidor);
                        }
                       
                    }
                    listUnidades.Add(newUnidad);
                }
                
            }

            return listUnidades;
        }
    }



    public enum Entidades
    {
        Edificio,
        Division,
        Servicio
    }
}
