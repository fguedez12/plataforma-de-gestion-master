﻿
using api_gestiona.DTOs;
using api_gestiona.DTOs.Documentos;
using api_gestiona.DTOs.Files;
using api_gestiona.DTOs.Pagination;
using api_gestiona.Entities;
using api_gestiona.Helpers;
using api_gestiona.Security;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace api_gestiona.Services
{
    public class DocumentosService : IDocumentosService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly Constants _constants;

        public DocumentosService(IConfiguration configuration, ApplicationDbContext context, IMapper mapper, Constants constants)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _constants = constants;
        }
        public async Task<string> Test()
        {
            return await Task.FromResult("Ok");
        }

        public async Task CheckReplaceDocumento(long servicioId, string userId, int tipoDocumento, int etapa)
        {
            var entity = await _context.Documentos
                .Where(x => x.ServicioId == servicioId && x.TipoDocumentoId == tipoDocumento 
                && x.Active && x.EtapaSEV_docs == etapa && x.CreatedAt.Year==DateTime.Now.Year).FirstOrDefaultAsync();
            if (entity is null)
            {
                return;
            }
            await DeleteDocumento(entity.Id, userId);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        public FileDTO SaveFile(IFormFile file)
        {
            string tokenName = Guid.NewGuid().ToString();
            var ruta = _configuration.GetValue<string>("DirectorioArchivosEV:documentos");
            var userName = _configuration.GetValue<string>("ImpersonalizacionSettings:Usuario");
            var password = _configuration.GetValue<string>("ImpersonalizacionSettings:Clave");
            var domain = _configuration.GetValue<string>("ImpersonalizacionSettings:Dominio");
            var ext = Path.GetExtension(file.FileName);
            string nombreArchivo = $"{ruta}\\{tokenName}{ext}";

            using (WindowsLogin wl = new WindowsLogin(userName!, domain!, password!))
            {
                WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {
                    using (var stream = new FileStream(nombreArchivo, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    };
                });
            };

            var response = new FileDTO { Nombre = nombreArchivo, NombreOriginal = file.FileName };

            return response;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        public async Task DeleteFile(int id)
        {
            var userName = _configuration.GetValue<string>("ImpersonalizacionSettings:Usuario");
            var password = _configuration.GetValue<string>("ImpersonalizacionSettings:Clave");
            var domain = _configuration.GetValue<string>("ImpersonalizacionSettings:Dominio");

            var actaFromDb = await _context.Documentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var adjunto = actaFromDb.AdjuntoUrl;
            using (WindowsLogin wl = new WindowsLogin(userName, domain, password))
            {
                WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {
                    if (File.Exists(adjunto))
                    {
                        File.Delete(adjunto);
                    }
                });
            };

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        public async Task DeleteFileInvitacion(int id)
        {
            var userName = _configuration.GetValue<string>("ImpersonalizacionSettings:Usuario");
            var password = _configuration.GetValue<string>("ImpersonalizacionSettings:Clave");
            var domain = _configuration.GetValue<string>("ImpersonalizacionSettings:Dominio");

            var actaFromDb = await _context.Documentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var invFdb = await _context.Documentos.AsNoTracking().Where(x => x.DocumentoPadreId == id && x.TipoAdjunto == "Invitacion").FirstOrDefaultAsync();
            var adjunto = invFdb.AdjuntoUrl;
            using (WindowsLogin wl = new WindowsLogin(userName, domain, password))
            {
                WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {
                    if (File.Exists(adjunto))
                    {
                        File.Delete(adjunto);
                    }
                });
            };

        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validar la compatibilidad de la plataforma", Justification = "<pendiente>")]
        public async Task DeleteFileFotografias(int id)
        {
            var userName = _configuration.GetValue<string>("ImpersonalizacionSettings:Usuario");
            var password = _configuration.GetValue<string>("ImpersonalizacionSettings:Clave");
            var domain = _configuration.GetValue<string>("ImpersonalizacionSettings:Dominio");

            var actaFromDb = await _context.Documentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var fotFdb = await _context.Documentos.AsNoTracking().Where(x => x.DocumentoPadreId == id && x.TipoAdjunto == "Fotografias").FirstOrDefaultAsync();
            var adjunto = fotFdb.AdjuntoUrl;
            using (WindowsLogin wl = new WindowsLogin(userName, domain, password))
            {
                WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {
                    if (File.Exists(adjunto))
                    {
                        File.Delete(adjunto);
                    }
                });
            };

        }

        public async Task<DocumentoResponse> SaveActa(FileDTO file, ActaDTO model, string userId)
        {
            var acta = _mapper.Map<ActaComite>(model);
            acta.AdjuntoUrl = file.Nombre;
            acta.AdjuntoNombre = file.NombreOriginal;
            acta.CreatedBy = userId;
            //acta.ServicioId = await GetServicioIdFromUserId(userId);
            acta.CreatedAt = DateTime.Now;
            acta.Active = true;
            _context.ActasComite.Add(acta);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveReunion(FileDTO file, ReunionDTO model, string userId)
        {
            var entity = _mapper.Map<ReunionComite>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ReunionesComite.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveListaIntegrantes(FileDTO file, ListaIntegrantesDTO model, string userId)
        {
            var entity = _mapper.Map<ListaIntegrante>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId =  await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ListaIntegrantes.Add(entity);
            await _context.SaveChangesAsync();
            //foreach (var integrante in model.Integrantes)
            //{
            //    integrante.ListaIntegranteId = entity.Id;
            //}
            //var listIntegrantes = _mapper.Map<List<Integrante>>(model.Integrantes);
            //_context.Integrantes.AddRange(listIntegrantes);
            //await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveProcReutilizacionPapel(FileDTO file, ProcReutilizacionPapelDTO model, string userId)
        {
            var entity = _mapper.Map<ProcedimientoReutilizacionPapel>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId =  await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ProcedimientoReutilizacionPapeles.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveGestionCompraSustentable(GestionCompraSustentableDTO model, string userId)
        {
            var entity = _mapper.Map<GestionCompraSustentable>(model);
            entity.CreatedBy = userId;
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.GestionCompraSustentables.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SavePolitica(FileDTO file, PoliticaDTO model, string userId,FileDTO fileRespParticipativo, FileDTO fileResp)
        {
            var entity = _mapper.Map<Politica>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            if ( !string.IsNullOrEmpty(fileRespParticipativo.Nombre)) 
            {
                entity.AdjuntoRespaldoUrlParticipativo = fileRespParticipativo.Nombre;
                entity.AdjuntoRespaldoNombreParticipativo = fileRespParticipativo.NombreOriginal;
            }
            
            if ( !string.IsNullOrEmpty(fileResp.Nombre)) { 
                entity.AdjuntoRespaldoUrl = fileResp.Nombre;
                entity.AdjuntoRespaldoNombre = fileResp.NombreOriginal;
            }
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.Politicas.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }
        public async Task<DocumentoResponse> SaveDifusion(FileDTO file, DifusionDTO model, string userId)
        {
            var entity = _mapper.Map<DifusionPolitica>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.DifusionPoliticas.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveProcPapel(FileDTO file, ProcedimientoPapelDTO model, string userId)
        {
            var entity = _mapper.Map<ProcedimientoPapel>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ProcedimientosPapel.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveProcResiduo(FileDTO file, ProcedimientoResiduoDTO model, string userId)
        {
            var entity = _mapper.Map<ProcedimientoResiduo>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ProcedimientosResiduo.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveProcResiduoSistema(FileDTO file, ProcedimientoResiduoSistemaDTO model, string userId)
        {
            var entity = _mapper.Map<ProcedimientoResiduoSistema>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ProcedimientosResiduoSistema.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }
        public async Task<DocumentoResponse> SaveProcBajaBienes(FileDTO file, ProcedimientoBajaBienesDTO model, string userId)
        {
            var entity = _mapper.Map<ProcedimientoBajaBienes>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ProcedimientosBajaBienes.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveProcCompraSustentables(FileDTO file, ProcedimientoCompraSustentableDTO model, string userId)
        {
            var entity = _mapper.Map<ProcedimientoCompraSustentable>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ProcedimientosCompraSustentable.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveCharlas(FileDTO file,CharlasDTO model, string userId)
        {
            var entity = _mapper.Map<Charla>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            //entity.ServicioId = await GetServicioIdFromUserId(userId);
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.Charlas.Add(entity);
            await _context.SaveChangesAsync(); 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveListadoColaborador(ListadoColaboradorDTO model, string userId, FileDTO? file = null)
        {
            var entity = _mapper.Map<ListadoColaborador>(model);
            if (file != null) 
            {
                entity.AdjuntoUrl = file.Nombre;
                entity.AdjuntoNombre = file.NombreOriginal;
            }
            entity.CreatedBy = userId;
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ListadoColaboradores.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveCapacitadosMP(FileDTO file, CapacitadosMPDTO model, string userId)
        {
            var entity = _mapper.Map<CapacitadosMP>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.CapacitadosMP.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SavePACE3(FileDTO file, FileDTO fileCompromiso, PacE3DTO model, string userId)
        {
            var entity = _mapper.Map<PacE3>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.AdjuntoRespaldoUrlCompromiso = fileCompromiso.Nombre;
            entity.AdjuntoRespaldoNombreCompromiso = fileCompromiso.NombreOriginal;
            entity.CreatedBy = userId;
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.PacE3.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveResolucionApruebaPlan(FileDTO file,ResolucionApruebaPlanDTO model, string userId)
        {
            var entity = _mapper.Map<ResolucionApruebaPlan>(model);
            entity.AdjuntoUrl = file.Nombre;
            entity.AdjuntoNombre = file.NombreOriginal;
            entity.CreatedBy = userId;
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.ResolucionApruebaPlan.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> UpdateActa([FromRoute] int id,FileDTO file, ActaDTO model, string userId)
        {
            var actaFromDb = await _context.ActasComite.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (actaFromDb==null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else 
            {
                model.AdjuntoUrl = actaFromDb.AdjuntoUrl;
                model.AdjuntoNombre = actaFromDb.AdjuntoNombre;
            }
   

            var acta = _mapper.Map<ActaComite>(model);
            acta.Id = id;
            acta.UpdatedAt = DateTime.Now;
            acta.ModifiedBy = userId;
            //acta.ServicioId = await GetServicioIdFromUserId(userId);
            acta.CreatedBy = actaFromDb.CreatedBy;
            acta.CreatedAt = actaFromDb.CreatedAt;
            acta.Active = true;
            try
            {
                _context.Update(acta);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<DocumentoResponse> UpdateReunion([FromRoute] int id, FileDTO file, ReunionDTO model, string userId)
        {
            var entity = await _context.ReunionesComite.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<ReunionComite>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            //entityToUpdate.ServicioId = await GetServicioIdFromUserId(userId);
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateListaIntegrantes([FromRoute] int id, FileDTO file, ListaIntegrantesDTO model, string userId)
        {
            var entity = await _context.ListaIntegrantes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var integrantes = await _context.Integrantes.Where(i => i.ListaIntegranteId == id).ToListAsync();
            _context.Integrantes.RemoveRange(integrantes);
            await _context.SaveChangesAsync();

            var entityToUpdate = _mapper.Map<ListaIntegrante>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateListadoColaboradores([FromRoute] int id, ListadoColaboradorDTO model, string userId, FileDTO? file=null)
        {
            var entity = await _context.ListadoColaboradores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (model.EtapaSEV_docs == 1)
            {
                if (!string.IsNullOrEmpty(file?.Nombre))
                {
                    model.AdjuntoUrl = file.Nombre;
                    model.AdjuntoNombre = file.NombreOriginal;
                }
                else
                {
                    model.AdjuntoUrl = entity.AdjuntoUrl;
                    model.AdjuntoNombre = entity.AdjuntoNombre;
                }
            }

            

            var entityToUpdate = _mapper.Map<ListadoColaborador>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateCapacitadosMP([FromRoute] int id, FileDTO file, CapacitadosMPDTO model, string userId)
        {
            var entity = await _context.CapacitadosMP.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<CapacitadosMP>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdatePacE3([FromRoute] int id, FileDTO file, FileDTO fileCompromiso, PacE3DTO model, string userId)
        {
            var entity = await _context.PacE3.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (string.IsNullOrEmpty(file.Nombre) && string.IsNullOrEmpty(fileCompromiso.Nombre))
            {
                if (!string.IsNullOrEmpty(file.Nombre))
                {
                    model.AdjuntoUrl = file.Nombre;
                    model.AdjuntoNombre = file.NombreOriginal;
                }
                else
                {
                    model.AdjuntoUrl = entity.AdjuntoUrl;
                    model.AdjuntoNombre = entity.AdjuntoNombre;
                }
                if (!string.IsNullOrEmpty(fileCompromiso.Nombre))
                {
                    model.AdjuntoRespaldoUrlCompromiso = fileCompromiso.Nombre;
                    model.AdjuntoRespaldoNombreCompromiso = fileCompromiso.NombreOriginal;
                }
                else
                {
                    model.AdjuntoRespaldoUrlCompromiso = entity.AdjuntoRespaldoUrlCompromiso;
                    model.AdjuntoRespaldoNombreCompromiso = entity.AdjuntoRespaldoNombreCompromiso;
                }
            }
            else 
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
                model.AdjuntoRespaldoUrlCompromiso = fileCompromiso.Nombre;
                model.AdjuntoRespaldoNombreCompromiso = fileCompromiso.NombreOriginal;

            }

            var entityToUpdate = _mapper.Map<PacE3>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateResolucionApruebaPlan([FromRoute] int id, FileDTO file,ResolucionApruebaPlanDTO model, string userId)
        {
            var entity = await _context.ResolucionApruebaPlan.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (string.IsNullOrEmpty(file.Nombre))
            {
                if (!string.IsNullOrEmpty(file.Nombre))
                {
                    model.AdjuntoUrl = file.Nombre;
                    model.AdjuntoNombre = file.NombreOriginal;
                }
                else
                {
                    model.AdjuntoUrl = entity.AdjuntoUrl;
                    model.AdjuntoNombre = entity.AdjuntoNombre;
                }
            }
            else 
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }

            var entityToUpdate = _mapper.Map<ResolucionApruebaPlan>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public async Task<DocumentoResponse> UpdateGestionCompraSustentable([FromRoute] int id,GestionCompraSustentableDTO model, string userId)
        {
            var entity = await _context.GestionCompraSustentables.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            var entityToUpdate = _mapper.Map<GestionCompraSustentable>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdatePolitica([FromRoute] int id, FileDTO file, FileDTO fileRP, FileDTO fileRespaldo, PoliticaDTO model, string userId)
        {
            var entity = await _context.Politicas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            if (!string.IsNullOrEmpty(fileRP.Nombre))
            {
                model.AdjuntoRespaldoUrlParticipativo = fileRP.Nombre;
                model.AdjuntoRespaldoNombreParticipativo = fileRP.NombreOriginal;
            }
            else
            {
                model.AdjuntoRespaldoUrlParticipativo = entity.AdjuntoRespaldoUrlParticipativo;
                model.AdjuntoRespaldoNombreParticipativo = entity.AdjuntoRespaldoNombreParticipativo;
            }

            if (!string.IsNullOrEmpty(fileRespaldo.Nombre))
            {
                model.AdjuntoRespaldoUrl = fileRespaldo.Nombre;
                model.AdjuntoRespaldoNombre = fileRespaldo.NombreOriginal;
            }
            else
            {
                model.AdjuntoRespaldoUrl = entity.AdjuntoRespaldoUrl;
                model.AdjuntoRespaldoNombre = entity.AdjuntoRespaldoNombre;
            }

            var entityToUpdate = _mapper.Map<Politica>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateDifusion([FromRoute] int id, FileDTO file, DifusionDTO model, string userId)
        {
            var entity = await _context.DifusionPoliticas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<DifusionPolitica>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateProcedimientoPapel([FromRoute] int id, FileDTO file, ProcedimientoPapelDTO model, string userId)
        {
            var entity = await _context.ProcedimientosPapel.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<ProcedimientoPapel>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateProcedimientoResiduo([FromRoute] int id, FileDTO file, ProcedimientoResiduoDTO model, string userId)
        {
            var entity = await _context.ProcedimientosResiduo.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<ProcedimientoResiduo>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateProcedimientoResiduoSistema([FromRoute] int id, FileDTO file, ProcedimientoResiduoSistemaDTO model, string userId)
        {
            var entity = await _context.ProcedimientosResiduoSistema.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<ProcedimientoResiduoSistema>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateProcedimientoBajaBienes([FromRoute] int id, FileDTO file, ProcedimientoBajaBienesDTO model, string userId)
        {
            var entity = await _context.ProcedimientosBajaBienes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<ProcedimientoBajaBienes>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateProcReutilizacionPapel([FromRoute] int id, FileDTO file, ProcReutilizacionPapelDTO model, string userId)
        {
            var entity = await _context.ProcedimientoReutilizacionPapeles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<ProcedimientoReutilizacionPapel>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateProcedimientoCompraSustentable([FromRoute] int id, FileDTO file, ProcedimientoCompraSustentableDTO model, string userId)
        {
            var entity = await _context.ProcedimientosCompraSustentable.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<ProcedimientoCompraSustentable>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> UpdateCharlas([FromRoute] int id, FileDTO file,CharlasDTO model, string userId)
        {
            var entity = await _context.Charlas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (entity == null)
            {
                return ReturnError("El recurso solicitado no existe");
            }

            if (!string.IsNullOrEmpty(file.Nombre))
            {
                model.AdjuntoUrl = file.Nombre;
                model.AdjuntoNombre = file.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entity.AdjuntoUrl;
                model.AdjuntoNombre = entity.AdjuntoNombre;
            }

            var entityToUpdate = _mapper.Map<Charla>(model);
            entityToUpdate.Id = id;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.ModifiedBy = userId;
            //entityToUpdate.ServicioId = await GetServicioIdFromUserId(userId);
            entityToUpdate.CreatedBy = entity.CreatedBy;
            entityToUpdate.CreatedAt = entity.CreatedAt;
            entityToUpdate.Active = true;
            try
            {
                _context.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                return new DocumentoResponse { Ok = true };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> GetComitePagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            try
            {
                var query = queryable.Pagin(paginationDTO);
                var entities = await query.ToListAsync();
                //var list = _mapper.Map<List<ComiteDTO>>(entities);
                var list = MapComite(entities.Cast<Documento>().ToList());
                list = list.OrderByDescending(x => x.Fecha).ToList();
                return new DocumentoResponse { Ok = true, Comites = list };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private List<ComiteDTO> MapComite(List<Documento> entities)
        {
            var response = new List<ComiteDTO>{};
            foreach (var item in entities){
                var comiteDto = new ComiteDTO{
                    Id = item.Id,
                    Nresolucion = item.Nresolucion,
                    TipoDocumentoId = item.TipoDocumentoId,
                    TipoDocumentoNombre = item.EtapaSEV_docs==2? item.TipoDocumento.NombreE2 : item.TipoDocumento.Nombre ,
                    Fecha = item.Fecha.Value
                };

                var listaTemas = new List<string>();
                if(item.ApruebaAlcanceGradualSEV==true){
                    listaTemas.Add("Aprueba alcance gradual SEV");
                }
                if(item.RevisionPoliticaAmbiental){
                    listaTemas.Add("Revisa existencia Política Ambiental");
                }
                if(item.DetActDeConcientizacion==true){
                    listaTemas.Add("Coordina actividades de concientización");
                }
                if(item.RevisionProcBienesMuebles==true){
                    listaTemas.Add("Revisa existencia procedimientos de bienes muebles");
                }
                if(item.ApruebaDiagnostico==true){
                    listaTemas.Add("Aprueba Diagnostico de Gestión Ambiental");
                }

                comiteDto.ListaTemas = listaTemas;
                response.Add(comiteDto);
            }

            return response;
        }

        public async Task<DocumentoResponse> GetPoliticaPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            try
            {
                var query = queryable.Pagin(paginationDTO); 
                var entities = await query.ToListAsync();
                var list = _mapper.Map<List<PoliticaListaDTO>>(entities);
                return new DocumentoResponse { Ok = true, PoliticaLista = list };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<DocumentoResponse> GetPgaPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            try
            {
                
                var query = queryable.Pagin(paginationDTO);
               
                var entities = await query.ToListAsync();
              
                var list = _mapper.Map<List<PgaListaDTO>>(entities);
                
                return new DocumentoResponse { Ok = true, PgaLista = list };
            }
            catch (Exception ex)
            {
               
                throw; // Relanzar la excepción original por ahora para mantener el comportamiento
            }

        }

        public async Task<DocumentoResponse> GetProcedimientosPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            try
            {
                var query = queryable.Pagin(paginationDTO);
                var entities = await query.ToListAsync();
                var list = _mapper.Map<List<ProcedimientoListaDto>>(entities);
                return new DocumentoResponse { Ok = true, Procedimientos = list };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> GetCharlasPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            try
            {
                var query = queryable.Pagin(paginationDTO);
                var entities = await query.ToListAsync();
                var list = _mapper.Map<List<CharlaListaDTO>>(entities);
                return new DocumentoResponse { Ok = true, Charlas = list };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> GetCapacitadosMPPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            try
            {
                var query = queryable.Pagin(paginationDTO);
                var entities = await query.ToListAsync();
                var list = _mapper.Map<List<CapacitadosMPDTO>>(entities);
                return new DocumentoResponse { Ok = true, CapacitadosMP = list };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> GetPacE3Pagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            try
            {
                var query = queryable.Pagin(paginationDTO);
                var entities = await query.ToListAsync();
                var list = _mapper.Map<List<PacE3DTO>>(entities);
                return new DocumentoResponse { Ok = true, PacE3s = list };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> GetCompraSustentalbesPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            try
            {
                var query = queryable.Pagin(paginationDTO);
                var entities = await query.ToListAsync();
                var list = _mapper.Map<List<GestionCompraSustentableDTO>>(entities);
                return new DocumentoResponse { Ok = true, CompraSustentables = list };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> GetActasComiteList(long id)
        {
            var lista = await GetList<ActaDTO>(id, _constants.TIPO_DOCUMENTO_ACTA, "Nresolucion");
            
            
            return new DocumentoResponse { Ok = true, Actas = lista };
           

        }

        public async Task<DocumentoResponse> GetPoliticaList(long id)
        {

            var lista = await GetList<PoliticaListaDTO>(id, _constants.TIPO_DOCUMENTO_POLITICA, "NResolucionPolitica");
            return new DocumentoResponse { Ok = true, PoliticaLista = lista };
            
        }

        private async Task<List<TDTO>> GetList<TDTO>(long id,int tipoDocumento, string property)  where TDTO : class
        {
            try
            {
                var propertyInfo = typeof(Documento).GetProperty(property);
                var query = _context.Documentos
                    .Where(a => a.Active && a.TipoDocumentoId == tipoDocumento && a.ServicioId == id)
                    .OrderByProperty(property)
                    .AsNoTracking()
                    .AsQueryable();

                var entities = await query.ToListAsync();
                var list = _mapper.Map<List<TDTO>>(entities);
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<DocumentoResponse> GetDocumentoById(int id)
        {
            var documento = await _context.Documentos.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (documento == null) {
                return ReturnError("No se encuentra el recurso solicitado");
            }
            var response = new DocumentoResponse { Ok = true };

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_ACTA)
            {
                var acta = await _context.ActasComite.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var actaDto = _mapper.Map<ActaDTO>(acta);
                response.Acta = actaDto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_REUNION)
            {
                var reunion = await _context.ReunionesComite.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var reunionDto = _mapper.Map<ReunionDTO>(reunion);
                response.Reunion = reunionDto;
            }
            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_LISTA_INTEGRANTES)
            {
                var listaIntegrantes = await _context.ListaIntegrantes
                        .Include(x=>x.Integrantes)
                        .FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var listaIntegrantesDto = _mapper.Map<ListaIntegrantesDTO>(listaIntegrantes);
                response.ListaIntegrante = listaIntegrantesDto;
            }
            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_POLITICA)
            {
                var entity = await _context.Politicas.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<PoliticaDTO>(entity);
                response.Politica = dto;
            }
            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_DIFUSION)
            {
                var entity = await _context.DifusionPoliticas.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<DifusionDTO>(entity);
                response.Difusion = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_PAPEL)
            {
                var entity = await _context.ProcedimientosPapel.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<ProcedimientoPapelDTO>(entity);
                response.ProcedimientoPapel = dto;
            }
            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO)
            {
                var entity = await _context.ProcedimientosResiduo.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<ProcedimientoResiduoDTO>(entity);
                response.ProcedimientoResiduo = dto;
            }
            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO_SISTEMA)
            {
                var entity = await _context.ProcedimientosResiduoSistema.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<ProcedimientoResiduoSistemaDTO>(entity);
                response.ProcedimientoResiduoSistema = dto;
            }
            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_BAJA_BIENES)
            {
                var entity = await _context.ProcedimientosBajaBienes.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<ProcedimientoBajaBienesDTO>(entity);
                response.ProcedimientoBajaBienes = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_COMPRA_SUSTENTABLE)
            {
                var entity = await _context.ProcedimientosCompraSustentable.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<ProcedimientoCompraSustentableDTO>(entity);
                response.ProcedimientoCompraSustentable = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROC_REUTILIZACION_PAPEL)
            {
                var entity = await _context.ProcedimientoReutilizacionPapeles.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<ProcReutilizacionPapelDTO>(entity);
                response.ProcedimientoReutilizacionPapel = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_CHARLA)
            {
                var entity = await _context.Charlas.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<CharlasDTO>(entity);
                response.Charla = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_CAPACITADOS_MP)
            {
                var entity = await _context.CapacitadosMP.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<CapacitadosMPDTO>(entity);
                response.CapacitadoMP = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PAC_E3)
            {
                var entity = await _context.PacE3.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<PacE3DTO>(entity);
                response.PacE3 = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_RESOLUCION_APRUEBA_PLAN)
            {
                var entity = await _context.ResolucionApruebaPlan.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<ResolucionApruebaPlanDTO>(entity);
                response.ResolucionApruebaPlanDTO = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_LISTADO_COLABORADORES)
            {
                var entity = await _context.ListadoColaboradores.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<ListadoColaboradorDTO>(entity);
                response.ListadoColaborador = dto;

            }
            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_GESTION_COMPRA_SUSTENTABLE)
            {
                var entity = await _context.GestionCompraSustentables.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<GestionCompraSustentableDTO>(entity);
                response.CompraSustentable = dto;
            }

            if (documento.TipoDocumentoId == _constants.TIPO_DOCUMENTO_INFORME_DA)
            {
                var entity = await _context.InformesDA.FirstOrDefaultAsync(x => x.Id == id && x.Active);
                var dto = _mapper.Map<InformeDADTO>(entity);
                response.InformeDA = dto;
            }

            return response;
        }

        public async Task<DocumentoResponse> DeleteDocumento(long id, string userId)
        {
            var documento = await _context.Documentos.FirstOrDefaultAsync(x => x.Id == id && x.Active);
            if (documento == null)
            {
                return ReturnError("No se encuentra el recurso solicitado");
            }

            documento.Active = false;
            documento.ModifiedBy = userId;
            documento.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<DocumentoResponse> SaveInformeDA(FileDTO file, InformeDADTO model, string userId)
        {
            var entity = _mapper.Map<InformeDA>(model);
            if (file != null)
            {
                entity.AdjuntoUrl = file.Nombre;
                entity.AdjuntoNombre = file.NombreOriginal;
            }
            entity.CreatedBy = userId;
            entity.CreatedAt = DateTime.Now;
            entity.Active = true;
            _context.InformesDA.Add(entity);
            await _context.SaveChangesAsync();
            return new DocumentoResponse { Ok = true };
        }

        public async Task<bool> CheckResolucionExist(string nresolucion, long servicioId)
        {
            var exist = await _context.ActasComite.AnyAsync(x=>x.Nresolucion.ToUpper()==nresolucion.ToUpper() && x.Active && x.ServicioId == servicioId);
            return exist;
        }


        public async Task<IQueryable<Documento>> QueryComite(long servicioId, int etapa)
        {
            var query = _context.Documentos
                   .Where(x => x.Active && x.EtapaSEV_docs== etapa && x.ServicioId == servicioId &&
                   (
                    x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_ACTA || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_REUNION ||
                    x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_LISTA_INTEGRANTES || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_INFORME_DA))
                   .Include(a => a.TipoDocumento)
                   .OrderByDescending(a => a.Fecha)
                   .AsNoTracking()
                   //.Pagin(paginationDTO)
                   .AsQueryable();

            //query = await ProcessQuery(paginationDTO, id, query);

            return query;
        }

        public async Task<IQueryable<Documento>> QueryPolitica(long servicioId,int etapa)
        {
            var query = _context.Documentos
                    .Where(x =>
                    x.Active && x.EtapaSEV_docs == etapa &&  x.ServicioId == servicioId &&
                    (x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_POLITICA || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_DIFUSION))
                    .Include(a => a.TipoDocumento)
                    .OrderByDescending(a => a.Fecha)
                    .AsNoTracking()
                    //.Pagin(paginationDTO)
                    .AsQueryable();

            //query = await ProcessQuery(paginationDTO, id, query);

            return query;
        }

        public async Task<IQueryable<Documento>> QueryPga(long servicioId,int etapa)
        {
            var query = _context.Documentos
                    .Where(x =>
                    x.Active && x.EtapaSEV_docs == etapa &&  x.ServicioId == servicioId &&
                    (x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_RESOLUCION_APRUEBA_PLAN))
                    .Include(a => a.TipoDocumento)
                    .OrderByDescending(a => a.Fecha)
                    .AsNoTracking()
                    //.Pagin(paginationDTO)
                    .AsQueryable();

            //query = await ProcessQuery(paginationDTO, id, query);

            return query;
        }

        public async Task<IQueryable<Documento>> QueryProcedimientos(long servicioId)
        {
            var query = _context.Documentos
                    .Where(x => x.Active && x.ServicioId == servicioId &&
                    (x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_PAPEL
                    || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO
                    || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO_SISTEMA
                    || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_BAJA_BIENES
                    || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_COMPRA_SUSTENTABLE
                    || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PROC_REUTILIZACION_PAPEL
                    ))
                    .Include(a => a.TipoDocumento)
                    .OrderByDescending(a => a.Fecha)
                    .AsNoTracking()
                    //.Pagin(paginationDTO)
                    .AsQueryable();

            //query = await ProcessQuery(paginationDTO, id, query);

            return query;
        }

        public async Task<IQueryable<Documento>> QueryCharlas(long servicioId, int etapa)
        {
            var query = _context.Documentos
                    .Where(x => x.Active && x.EtapaSEV_docs == etapa && x.ServicioId == servicioId && x.DocumentoPadreId == null &&
                    (
                    x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_CHARLA || x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_LISTADO_COLABORADORES
                    ))
                    .Include(a => a.TipoDocumento)
                    .OrderByDescending(a => a.Fecha)
                    .AsNoTracking()
                    //.Pagin(paginationDTO)
                    .AsQueryable();
            //query = await ProcessQuery(paginationDTO, id,query);
            return query;
        }

        public async Task<IQueryable<Documento>> QueryCapacitadosMP(long servicioId, int etapa)
        {
            var query = _context.Documentos
                    .Where(x => x.Active && x.EtapaSEV_docs == etapa && x.ServicioId == servicioId && x.DocumentoPadreId == null &&
                    (
                    x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_CAPACITADOS_MP
                    ))
                    .Include(a => a.TipoDocumento)
                    .OrderByDescending(a => a.Fecha)
                    .AsNoTracking()
                    //.Pagin(paginationDTO)
                    .AsQueryable();
            //query = await ProcessQuery(paginationDTO, id,query);
            return query;
        }
        public IQueryable<Documento> QueryPacE3(long servicioId, int etapa)
        {
            var query = _context.Documentos
                    .Where(x => x.Active && x.EtapaSEV_docs == etapa && x.ServicioId == servicioId && x.DocumentoPadreId == null &&
                    (
                    x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_PAC_E3
                    ))
                    .Include(a => a.TipoDocumento)
                    .OrderByDescending(a => a.Fecha)
                    .AsNoTracking()
                    //.Pagin(paginationDTO)
                    .AsQueryable();
            //query = await ProcessQuery(paginationDTO, id,query);
            return query;
        }


        public IQueryable<Documento> QueryCompraSustentable(long servicioId)
        {
            var query = _context.Documentos
                    .Where(x => x.Active && x.ServicioId == servicioId && x.DocumentoPadreId == null &&
                    (
                    x.TipoDocumentoId == _constants.TIPO_DOCUMENTO_GESTION_COMPRA_SUSTENTABLE
                    ))
                    .Include(a => a.TipoDocumento)
                    .OrderByDescending(a => a.Fecha)
                    .AsNoTracking()
                    .AsQueryable();
            return query;
        }

        //private async Task<IQueryable<T>> ProcessQuery<T>(PaginationDTO paginationDTO, string id,IQueryable<T> queryable) where T : Documento
        //{
        //    var user = await _manager.FindByIdAsync(id);
        //    var isAdmin = await _manager.IsInRoleAsync(user, "ADMINISTRADOR");
        //    var servicioId = await _context.UsuariosServicios.Where(x => x.UsuarioId == id).Select(x => x.ServicioId).FirstOrDefaultAsync();

        //    if (!isAdmin)
        //    {
        //        queryable = queryable.Where(x => x.ServicioId == servicioId);
        //    }

        //    if (paginationDTO.ServicioId != null)
        //    {
        //        //Search Parameter
        //        queryable = queryable.Where(x => x.ServicioId == paginationDTO.ServicioId);
        //    }

        //    if (!string.IsNullOrEmpty(paginationDTO.SearchText))
        //    {
        //        //Search Parameter
        //        //queryable = queryable.Where(u => u.Nombre.Contains(paginationDTO.SearchText));
        //    }

        //    return queryable;
        //}

        public DocumentoResponse ReturnError(string msj)
        {
            return new DocumentoResponse { Ok = false, Msj = msj };
        }

        private async Task<long> GetServicioIdFromUserId(string userId)
        {
            var servicioId = await _context.UsuariosServicios.Where(x=>x.UsuarioId == userId).Select(x=>x.ServicioId).FirstOrDefaultAsync();
            return servicioId;
        }
    }
}
