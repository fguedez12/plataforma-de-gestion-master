using api_gestiona.DTOs;
using api_gestiona.DTOs.Documentos;
using api_gestiona.DTOs.Files;
using api_gestiona.DTOs.Pagination;
using api_gestiona.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Services.Contracts
{
    public interface IDocumentosService
    {
        Task<bool> CheckResolucionExist(string nresolucion, long servicioId);
        Task<DocumentoResponse> DeleteDocumento(long id, string userId);
        Task DeleteFile(int id);
        Task<DocumentoResponse> GetDocumentoById(int id);
        Task<DocumentoResponse> GetActasComiteList(long id);
        Task<DocumentoResponse> GetComitePagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO);
        DocumentoResponse ReturnError(string msj);
        Task<DocumentoResponse> SaveActa(FileDTO file, ActaDTO model, string userId);
        FileDTO SaveFile(IFormFile file);
        Task<DocumentoResponse> SaveReunion(FileDTO file, ReunionDTO model, string userId);
        Task<string> Test();
        Task<DocumentoResponse> UpdateActa([FromRoute] int id, FileDTO file, ActaDTO model, string userId);
        Task<DocumentoResponse> UpdateReunion([FromRoute] int id, FileDTO file, ReunionDTO model, string userId);
        Task<DocumentoResponse> SaveListaIntegrantes(FileDTO file, ListaIntegrantesDTO model, string userId);
        Task<DocumentoResponse> UpdateListaIntegrantes([FromRoute] int id, FileDTO file, ListaIntegrantesDTO model, string userId);
        Task<DocumentoResponse> GetPoliticaPagin<T>(IQueryable<T> queriable, PaginationDTO paginationDTO);
        Task<DocumentoResponse> GetPgaPagin<T>(IQueryable<T> queriable, PaginationDTO paginationDTO);
        Task<DocumentoResponse> GetPoliticaList(long id);
        Task<DocumentoResponse> SaveDifusion(FileDTO file, DifusionDTO model, string userId);
        Task<DocumentoResponse> UpdateDifusion([FromRoute] int id, FileDTO file, DifusionDTO model, string userId);
        Task<DocumentoResponse> SaveProcPapel(FileDTO file, ProcedimientoPapelDTO model, string userId);
        Task<DocumentoResponse> GetProcedimientosPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO);
        Task<DocumentoResponse> UpdateProcedimientoPapel([FromRoute] int id, FileDTO file, ProcedimientoPapelDTO model, string userId);
        Task<DocumentoResponse> SaveProcResiduo(FileDTO file, ProcedimientoResiduoDTO model, string userId);
        Task<DocumentoResponse> UpdateProcedimientoResiduo([FromRoute] int id, FileDTO file, ProcedimientoResiduoDTO model, string userId);
        Task<DocumentoResponse> SaveProcResiduoSistema(FileDTO file, ProcedimientoResiduoSistemaDTO model, string userId);
        Task<DocumentoResponse> UpdateProcedimientoResiduoSistema([FromRoute] int id, FileDTO file, ProcedimientoResiduoSistemaDTO model, string userId);
        Task<DocumentoResponse> SaveProcBajaBienes(FileDTO file, ProcedimientoBajaBienesDTO model, string userId);
        Task<DocumentoResponse> UpdateProcedimientoBajaBienes([FromRoute] int id, FileDTO file, ProcedimientoBajaBienesDTO model, string userId);
        Task<DocumentoResponse> SaveProcCompraSustentables(FileDTO file, ProcedimientoCompraSustentableDTO model, string userId);
        Task<DocumentoResponse> UpdateProcedimientoCompraSustentable([FromRoute] int id, FileDTO file, ProcedimientoCompraSustentableDTO model, string userId);
        Task<DocumentoResponse> SaveCharlas(FileDTO file,CharlasDTO model, string userId);
        Task<DocumentoResponse> GetCharlasPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO);
        Task DeleteFileInvitacion(int id);
        Task DeleteFileFotografias(int id);
        Task<DocumentoResponse> UpdateCharlas([FromRoute] int id, FileDTO file, CharlasDTO model, string userId);
        Task<IQueryable<Documento>> QueryComite(long servicioId, int etapa);
        Task<IQueryable<Documento>> QueryPolitica(long servicioId, int etapa);
        Task<IQueryable<Documento>> QueryPga(long servicioId, int etapa);
        Task<IQueryable<Documento>> QueryProcedimientos(long servicioId);
        Task<IQueryable<Documento>> QueryCharlas(long servicioId, int etapa);
        Task<DocumentoResponse> SaveListadoColaborador(ListadoColaboradorDTO model, string userId, FileDTO? file=null);
        Task<DocumentoResponse> UpdateListadoColaboradores([FromRoute] int id,ListadoColaboradorDTO model, string userId, FileDTO? file = null);
        Task<DocumentoResponse> SaveProcReutilizacionPapel(FileDTO file, ProcReutilizacionPapelDTO model, string userId);
        Task<DocumentoResponse> UpdateProcReutilizacionPapel([FromRoute] int id, FileDTO file, ProcReutilizacionPapelDTO model, string userId);
        Task<DocumentoResponse> SaveCapacitadosMP(FileDTO file, CapacitadosMPDTO model, string userId);
        Task<DocumentoResponse> UpdateCapacitadosMP([FromRoute] int id, FileDTO file, CapacitadosMPDTO model, string userId);
        Task<IQueryable<Documento>> QueryCapacitadosMP(long servicioId, int etapa);
        Task<DocumentoResponse> GetCapacitadosMPPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO);
        Task CheckReplaceDocumento(long servicioId, string userId, int tipoDocumento, int etapa);
        Task<DocumentoResponse> SaveGestionCompraSustentable(GestionCompraSustentableDTO model, string userId);
        IQueryable<Documento> QueryCompraSustentable(long servicioId);
        Task<DocumentoResponse> GetCompraSustentalbesPagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO);
        Task<DocumentoResponse> UpdateGestionCompraSustentable([FromRoute] int id, GestionCompraSustentableDTO model, string userId);
        Task<DocumentoResponse> UpdatePolitica([FromRoute] int id, FileDTO file, FileDTO fileRP, FileDTO fileRespaldo, PoliticaDTO model, string userId);
        Task<DocumentoResponse> SavePolitica(FileDTO file, PoliticaDTO model, string userId, FileDTO fileRespParticipativo, FileDTO fileResp);
        Task<DocumentoResponse> SavePACE3(FileDTO file, FileDTO fileCompromiso, PacE3DTO model, string userId);
        Task<DocumentoResponse> SaveResolucionApruebaPlan(FileDTO file, ResolucionApruebaPlanDTO model, string userId);
        IQueryable<Documento> QueryPacE3(long servicioId, int etapa);
        Task<DocumentoResponse> GetPacE3Pagin<T>(IQueryable<T> queryable, PaginationDTO paginationDTO);
        Task<DocumentoResponse> UpdatePacE3([FromRoute] int id, FileDTO file, FileDTO fileCompromiso, PacE3DTO model, string userId);
        Task<DocumentoResponse> UpdateResolucionApruebaPlan([FromRoute] int id, FileDTO file, ResolucionApruebaPlanDTO model, string userId);
        Task<DocumentoResponse> SaveInformeDA(FileDTO file, InformeDADTO model, string userId);
    }
}
