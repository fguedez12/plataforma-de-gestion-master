using api_gestiona.DTOs;
using api_gestiona.DTOs.Documentos;
using api_gestiona.DTOs.Files;
using api_gestiona.DTOs.Pagination;
using api_gestiona.Entities;
using api_gestiona.Helpers;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DocumentosController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IDocumentosService _documentosService;
        private readonly UserManager<Usuario> _manager;
        private readonly Constants _constants;

        public DocumentosController(ApplicationDbContext context, IMapper mapper, IDocumentosService documentosService, UserManager<Usuario> manager, Constants constants) : base(mapper, context, manager)
        {
            _context = context;
            _documentosService = documentosService;
            _manager = manager;
            _constants = constants;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            return await _documentosService.Test();
        }

        [HttpGet("comite")]
        public async Task<ActionResult<DocumentoResponse>> GetActasComite([FromQuery] PaginationDTO paginationDTO)
        {
            if (paginationDTO.ServicioId == null)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var servicioId = Convert.ToInt64(paginationDTO.ServicioId);
            if (!await userInService(userId, servicioId))
            {
                return _documentosService.ReturnError("El usuario no corresponde al servicio");
            }

            var queryable = await _documentosService.QueryComite(servicioId, paginationDTO.Etapa);
            if (paginationDTO.AnioDoc > 0)
            {
                queryable = queryable.Where(x => x.CreatedAt.Year == paginationDTO.AnioDoc);
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = await _documentosService.GetComitePagin(queryable, paginationDTO);
            var servicio = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == servicioId);
            if (servicio == null)
            {
                return NotFound();
            }
            response.NoRegistraPoliticaAmbiental = servicio.NoRegistraPoliticaAmbiental;
            response.NoRegistraDifusionInterna = servicio.NoRegistraDifusionInterna;
            response.NoRegistraActividadInterna = servicio.NoRegistraActividadInterna;
            response.NoRegistraReutilizacionPapel = servicio.NoRegistraReutilizacionPapel;
            response.NoRegistraProcFormalPapel = servicio.NoRegistraProcFormalPapel;
            response.NoRegistraDocResiduosCertificados = servicio.NoRegistraDocResiduosCertificados;
            response.NoRegistraDocResiduosSistemas = servicio.NoRegistraDocResiduosSistemas;
            response.NoRegistraProcBajaBienesMuebles = servicio.NoRegistraProcBajaBienesMuebles;
            response.NoRegistraProcComprasSustentables = servicio.NoRegistraProcComprasSustentables;


            return response;
        }


        [HttpGet("politica")]
        public async Task<DocumentoResponse> GetPoliticas([FromQuery] PaginationDTO paginationDTO)
        {
            if (paginationDTO.ServicioId == null)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var servicioId = Convert.ToInt64(paginationDTO.ServicioId);
            if (!await userInService(userId, servicioId))
            {
                return _documentosService.ReturnError("El usuario no corresponde al servicio");
            }
            var queryable = await _documentosService.QueryPolitica(servicioId, paginationDTO.Etapa);
            if (paginationDTO.AnioDoc > 0)
            {
                queryable = queryable.Where(x => x.CreatedAt.Year == paginationDTO.AnioDoc);
            }

            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = await _documentosService.GetPoliticaPagin(queryable, paginationDTO);
            return response;
        }

        [HttpGet("pga")]
        public async Task<DocumentoResponse> GetPga([FromQuery] PaginationDTO paginationDTO)
        {
            if (paginationDTO.ServicioId == null)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var servicioId = Convert.ToInt64(paginationDTO.ServicioId);
            if (!await userInService(userId, servicioId))
            {
                return _documentosService.ReturnError("El usuario no corresponde al servicio");
            }
            var queryable = await _documentosService.QueryPga(servicioId, paginationDTO.Etapa);
            if (paginationDTO.AnioDoc > 0)
            {
                queryable = queryable.Where(x => x.CreatedAt.Year == paginationDTO.AnioDoc);
            }

            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = await _documentosService.GetPgaPagin(queryable, paginationDTO);
            return response;
        }

        [HttpGet("procedimientos")]
        public async Task<DocumentoResponse> GetProcedimientos([FromQuery] PaginationDTO paginationDTO)
        {
            if (paginationDTO.ServicioId == null)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var servicioId = Convert.ToInt64(paginationDTO.ServicioId.Value);
            if (!await userInService(userId, servicioId))
            {
                return _documentosService.ReturnError("El usuario no corresponde al servicio");
            }
            var queryable = await _documentosService.QueryProcedimientos(servicioId);
            if (paginationDTO.AnioDoc > 0)
            {
                queryable = queryable.Where(x => x.CreatedAt.Year == paginationDTO.AnioDoc);
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = await _documentosService.GetProcedimientosPagin(queryable, paginationDTO);
            return response;
        }

        [HttpGet("charlas")]
        public async Task<DocumentoResponse> GetCharlas([FromQuery] PaginationDTO paginationDTO)
        {
            if (paginationDTO.ServicioId == null)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var servicioId = Convert.ToInt64(paginationDTO.ServicioId);
            if (!await userInService(userId, servicioId))
            {
                return _documentosService.ReturnError("El usuario no corresponde al servicio");
            }
            var queryable = await _documentosService.QueryCharlas(servicioId,paginationDTO.Etapa);
            if (paginationDTO.AnioDoc > 0)
            {
                queryable = queryable.Where(x => x.CreatedAt.Year == paginationDTO.AnioDoc);
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = await _documentosService.GetCharlasPagin(queryable, paginationDTO);
            return response;
        }

        [HttpGet("capacitados-mp")]
        public async Task<DocumentoResponse> GetCapacitadosMP([FromQuery] PaginationDTO paginationDTO)
        {
            if (paginationDTO.ServicioId == null)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var servicioId = Convert.ToInt64(paginationDTO.ServicioId);
            if (!await userInService(userId, servicioId))
            {
                return _documentosService.ReturnError("El usuario no corresponde al servicio");
            }
            var queryable = await _documentosService.QueryCapacitadosMP(servicioId, paginationDTO.Etapa);
            if (paginationDTO.AnioDoc > 0)
            {
                queryable = queryable.Where(x => x.CreatedAt.Year == paginationDTO.AnioDoc);
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = await _documentosService.GetCapacitadosMPPagin(queryable, paginationDTO);
            return response;
        }

        [HttpGet("pac-e3")]
        public async Task<DocumentoResponse> GetPacE3([FromQuery] PaginationDTO paginationDTO)
        {
            if (paginationDTO.ServicioId == null)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var servicioId = Convert.ToInt64(paginationDTO.ServicioId);
            if (!await userInService(userId, servicioId))
            {
                return _documentosService.ReturnError("El usuario no corresponde al servicio");
            }
            var queryable = _documentosService.QueryPacE3(servicioId, paginationDTO.Etapa);
            if (paginationDTO.AnioDoc > 0)
            {
                queryable = queryable.Where(x => x.CreatedAt.Year == paginationDTO.AnioDoc);
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = await _documentosService.GetPacE3Pagin(queryable, paginationDTO);
            return response;
        }

        [HttpGet("compra-sustentables")]
        public async Task<DocumentoResponse> GetCompraSustentables([FromQuery] PaginationDTO paginationDTO)
        {
            if (paginationDTO.ServicioId == null)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var servicioId = Convert.ToInt64(paginationDTO.ServicioId);
            if (!await userInService(userId, servicioId))
            {
                return _documentosService.ReturnError("El usuario no corresponde al servicio");
            }
            var queryable =  _documentosService.QueryCompraSustentable(servicioId);
            if (paginationDTO.AnioDoc > 0)
            {
                queryable = queryable.Where(x => x.CreatedAt.Year == paginationDTO.AnioDoc);
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = await _documentosService.GetCompraSustentalbesPagin(queryable, paginationDTO);
            return response;
        }

        [HttpGet("actas-comite-lista/{servicioId}")]
        public async Task<DocumentoResponse> GetActasComiteLista([FromRoute] long servicioId)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var inService = await userInService(userId, servicioId);
            if (!inService)
            {
                return new DocumentoResponse { Ok = false, Msj = "El usuario no pertenece al servicio" };
            }
            var response = await _documentosService.GetActasComiteList(servicioId);
            return response;
        }

        [HttpGet("politicas-lista/{servicioId}")]
        public async Task<DocumentoResponse> GetpoliticaLista([FromRoute] long servicioId)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var inService = await userInService(userId, servicioId);
            if (!inService)
            {
                return new DocumentoResponse { Ok = false, Msj = "El usuario no pertenece al servicio" };
            }
            var response = await _documentosService.GetPoliticaList(servicioId);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<DocumentoResponse> GetDocumentoById([FromRoute] int id)
        {
            var response = await _documentosService.GetDocumentoById(id);
            return response;

        }

        [HttpPost("acta-comite")]
        public async Task<ActionResult<DocumentoResponse>> PostActa([FromForm] ActaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;

            //await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_ACTA, model.EtapaSEV_docs);

            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }


            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);

            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveActa(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("reunion-comite")]
        public async Task<ActionResult<DocumentoResponse>> PostReunion([FromForm] ReunionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }
            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveReunion(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("lista-integrantes-comite")]
        public async Task<ActionResult<DocumentoResponse>> PostListaIntegrantes([FromForm] ListaIntegrantesDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }
            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveListaIntegrantes(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("procedimiento-reutilizacion-papel")]
        public async Task<ActionResult<DocumentoResponse>> PostProcedimientoReutilizacionPapel([FromForm] ProcReutilizacionPapelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_PROC_REUTILIZACION_PAPEL, model.EtapaSEV_docs);

            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);

            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveProcReutilizacionPapel(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("gestion-compra-sustentable")]
        public async Task<ActionResult<DocumentoResponse>> PostGestionCompraSustentable([FromForm] GestionCompraSustentableDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            // if (model.Adjunto is null)
            // {
            //     return _documentosService.ReturnError("No se encontró archivo adjunto");
            // }
            // if (model.AdjuntoCompraSustentableAnt is null)
            // {
            //     return _documentosService.ReturnError("No se encontró archivo adjunto");
            // }
            // if (model.AdjuntoCompraFuera is null)
            // {
            //     return _documentosService.ReturnError("No se encontró archivo adjunto");
            // }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_GESTION_COMPRA_SUSTENTABLE, model.EtapaSEV_docs);

            // var fileBytes = Convert.FromBase64String(model.Adjunto);
            // var filename = Path.GetFileName(model.AdjuntoPath);
            // MemoryStream stream = new MemoryStream(fileBytes);
            // IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);

            // var fileBytesCompraAnt = Convert.FromBase64String(model.AdjuntoCompraSustentableAnt);
            // var filenameCompraAnt = Path.GetFileName(model.AdjuntoPathCompraSustentableAnt);
            // MemoryStream streamCompraAnt = new MemoryStream(fileBytesCompraAnt);
            // IFormFile fileCompraAnt = new FormFile(streamCompraAnt, 0, fileBytesCompraAnt.Length, filenameCompraAnt!, filenameCompraAnt!);

            // var fileBytesCompraFuera = Convert.FromBase64String(model.AdjuntoCompraFuera);
            // var filenameCompraFuera = Path.GetFileName(model.AdjuntoPathCompraFuera);
            // MemoryStream streamCompraFuera = new MemoryStream(fileBytesCompraFuera);
            // IFormFile fileCompraFuera = new FormFile(streamCompraFuera, 0, fileBytesCompraFuera.Length, filenameCompraFuera!, filenameCompraFuera!);

            // var fileDto = _documentosService.SaveFile(file);
            // var fileDtoCompraAnt = _documentosService.SaveFile(fileCompraAnt);
            // var fileDtoCompraFuera = _documentosService.SaveFile(fileCompraFuera);
            await _documentosService.SaveGestionCompraSustentable(model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("politica")]
        public async Task<ActionResult<DocumentoResponse>> PostPolitica([FromForm] PoliticaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }


            // if (model.AdjuntoRespaldoParticipativo is null && model.EtapaSEV_docs == 2)
            // {
            //     return _documentosService.ReturnError("No se encontró archivo adjunto");
            // }

            var userId = User.Claims.First(i => i.Type == "userId").Value;

            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_POLITICA, model.EtapaSEV_docs);

            //var file = Request.Form.Files[0];
            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
            var fileDto = _documentosService.SaveFile(file);


            IFormFile? fileRespaldoParticipativo = null;
            FileDTO fileDtoRespaldoParticipativo = new FileDTO();
            if (model.AdjuntoRespaldoParticipativo is not null)
            {
                var fileBytesRP = Convert.FromBase64String(model.AdjuntoRespaldoParticipativo);
                var filenameRP = Path.GetFileName(model.AdjuntoRespaldoPathParticipativo);
                MemoryStream streamRespaldoParticipativo = new MemoryStream(fileBytesRP);
                fileRespaldoParticipativo = new FormFile(streamRespaldoParticipativo, 0, fileBytesRP.Length, filenameRP!, filenameRP!);
                fileDtoRespaldoParticipativo = _documentosService.SaveFile(fileRespaldoParticipativo);
            }

            IFormFile? fileRespaldo = null;
            FileDTO fileRespaldoDTO = new FileDTO();
            if (model.AdjuntoRespaldo is not null)
            {
                //fileRespaldo = Request.Form.Files[1];
                var fileBytesRespaldo = Convert.FromBase64String(model.AdjuntoRespaldo);
                var filenameRespaldo = Path.GetFileName(model.AdjuntoRespaldoPath!);
                MemoryStream streamRespaldo = new MemoryStream(fileBytesRespaldo);
                fileRespaldo = new FormFile(streamRespaldo, 0, fileBytesRespaldo.Length, filenameRespaldo!, filenameRespaldo!);
                fileRespaldoDTO = _documentosService.SaveFile(fileRespaldo);
            }
            await _documentosService.SavePolitica(fileDto, model, userId,fileDtoRespaldoParticipativo, fileRespaldoDTO);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("difusion")]
        public async Task<ActionResult<DocumentoResponse>> PostDifusion([FromForm] DifusionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (Request.Form.Files.Count == 0)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;

            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            var file = Request.Form.Files[0];
            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveDifusion(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }
        [HttpPost("procedimiento-papel")]
        public async Task<ActionResult<DocumentoResponse>> PostPolitica([FromForm] ProcedimientoPapelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_PAPEL, model.EtapaSEV_docs);

            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveProcPapel(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("procedimiento-residuo")]
        public async Task<ActionResult<DocumentoResponse>> PostProcedimientoResiduo([FromForm] ProcedimientoResiduoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;

            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

           // await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO, model.EtapaSEV_docs);

            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveProcResiduo(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("procedimiento-residuo-sistema")]
        public async Task<ActionResult<DocumentoResponse>> PostProcedimientoResiduoSistema([FromForm] ProcedimientoResiduoSistemaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            //await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO_SISTEMA,model.EtapaSEV_docs);

            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);

            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveProcResiduoSistema(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("procedimiento-baja-bienes")]
        public async Task<ActionResult<DocumentoResponse>> PostProcedimientoBajaBienes([FromForm] ProcedimientoBajaBienesDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_BAJA_BIENES, model.EtapaSEV_docs);

            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);

            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveProcBajaBienes(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("procedimiento-compra-sustentable")]
        public async Task<ActionResult<DocumentoResponse>> PostProcedimientoCompraSustentable([FromForm] ProcedimientoCompraSustentableDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;

            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_PROCEDIMIENTO_COMPRA_SUSTENTABLE, model.EtapaSEV_docs);

            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);

            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveProcCompraSustentables(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("charlas")]
        public async Task<ActionResult<DocumentoResponse>> PostCharlas([FromForm] CharlasDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            //await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_CHARLA, model.EtapaSEV_docs);

            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
            var fileDto = _documentosService.SaveFile(file);
            //var fileInvitacion = Request.Form.Files.Where(x => x.Name == "adjuntoInvitacion").FirstOrDefault();
            //var fileDtoInvitacion = _documentosService.SaveFile(fileInvitacion);
            await _documentosService.SaveCharlas(fileDto,model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("listado-colaboradores")]
        public async Task<ActionResult<DocumentoResponse>> PostListadoColaboradores([FromForm] ListadoColaboradorDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            bool esFechaValida = DateTime.Now <= new DateTime(2025, 7, 18);

            if (model.Adjunto is null && !esFechaValida && model.EtapaSEV_docs==1)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            if (model.EtapaSEV_docs == 1)
            {
                if (model.Adjunto != null)
                {
                    await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_LISTADO_COLABORADORES, model.EtapaSEV_docs);
                    var fileBytes = Convert.FromBase64String(model.Adjunto);
                    var filename = Path.GetFileName(model.AdjuntoPath);
                    MemoryStream stream = new MemoryStream(fileBytes);
                    IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                    var fileDto = _documentosService.SaveFile(file);
                    await _documentosService.SaveListadoColaborador(model, userId, fileDto);
                }
                else
                {
                    await _documentosService.SaveListadoColaborador(model, userId);
                }
                
            }
            else if (model.EtapaSEV_docs == 2)
            {
                await _documentosService.SaveListadoColaborador(model, userId);
            }

            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("capacitados-mp")]
        public async Task<ActionResult<DocumentoResponse>> PostCapacitadosMP([FromForm] CapacitadosMPDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("No se encontró archivo adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            await _documentosService.CheckReplaceDocumento(model.ServicioId, userId, _constants.TIPO_DOCUMENTO_CAPACITADOS_MP, model.EtapaSEV_docs);

            var fileBytes = Convert.FromBase64String(model.Adjunto);
            var filename = Path.GetFileName(model.AdjuntoPath);
            MemoryStream stream = new MemoryStream(fileBytes);
            IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
            //var file = Request.Form.Files.Where(x => x.Name == "adjunto").FirstOrDefault();
            var fileDto = _documentosService.SaveFile(file);
            await _documentosService.SaveCapacitadosMP(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPost("pac-e3")]
        public async Task<ActionResult<DocumentoResponse>> PostPacE3([FromForm] PacE3DTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null && model.AdjuntoRespaldoCompromiso is null)
            {
                return _documentosService.ReturnError("Debe venir al menos un adjunto");
            }

            if (model.Adjunto is not null && model.AdjuntoRespaldoCompromiso is not null)
            {
                return _documentosService.ReturnError("solo debe haber un adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            var fileDto = new FileDTO();
            if (model.Adjunto is not null) 
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var fileDtoCompromiso = new FileDTO();
            if (model.AdjuntoRespaldoCompromiso is not null)
            {
                var fileBytesCompromiso = Convert.FromBase64String(model.AdjuntoRespaldoCompromiso);
                var filenameCompromiso = Path.GetFileName(model.AdjuntoRespaldoPathCompromiso);
                MemoryStream streamCompromiso = new MemoryStream(fileBytesCompromiso);
                IFormFile fileCompromiso = new FormFile(streamCompromiso, 0, fileBytesCompromiso.Length, filenameCompromiso!, filenameCompromiso!);
                //var file = Request.Form.Files.Where(x => x.Name == "adjunto").FirstOrDefault();
                fileDtoCompromiso = _documentosService.SaveFile(fileCompromiso);
            }
  
            await _documentosService.SavePACE3(fileDto, fileDtoCompromiso, model, userId);
            return new DocumentoResponse { Ok = true };
        }

         [HttpPost("resolucion-aprueba-plan")]
        public async Task<ActionResult<DocumentoResponse>> ResolucionApruebaPlan([FromForm] ResolucionApruebaPlanDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            if (model.Adjunto is null)
            {
                return _documentosService.ReturnError("Debe venir al menos un adjunto");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            var fileDto = new FileDTO();
            if (model.Adjunto is not null) 
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            await _documentosService.SaveResolucionApruebaPlan(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("acta-comite/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutActa([FromRoute] int id, [FromForm] ActaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateActa(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("reunion-comite/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutReunion([FromRoute] int id, [FromForm] ReunionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateReunion(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }
        [HttpPut("politica/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutPolitica([FromRoute] int id, [FromForm] PoliticaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null) 
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            IFormFile fileRespaldoParticipativo;
            FileDTO fileDtoRespaldoParticipativo = new FileDTO();
            if (model.AdjuntoRespaldoParticipativo is not null)
            {
                var fileBytesRespaldoParticipativo = Convert.FromBase64String(model.AdjuntoRespaldoParticipativo);
                var filenameRespaldoParticipativo = Path.GetFileName(model.AdjuntoRespaldoPathParticipativo);
                MemoryStream stream = new MemoryStream(fileBytesRespaldoParticipativo);
                fileRespaldoParticipativo = new FormFile(stream, 0, fileBytesRespaldoParticipativo.Length, filenameRespaldoParticipativo!, filenameRespaldoParticipativo!);
                fileDtoRespaldoParticipativo = _documentosService.SaveFile(fileRespaldoParticipativo);
            }
            IFormFile fileRespaldo;
            FileDTO fileDtoRespaldo = new FileDTO();
            if (model.AdjuntoRespaldo is not null)
            {
                var fileBytesRespaldo = Convert.FromBase64String(model.AdjuntoRespaldo);
                var filenameRespaldo = Path.GetFileName(model.AdjuntoRespaldoPath);
                MemoryStream stream = new MemoryStream(fileBytesRespaldo);
                fileRespaldo = new FormFile(stream, 0, fileBytesRespaldo.Length, filenameRespaldo!, filenameRespaldo!);
                fileDtoRespaldo = _documentosService.SaveFile(fileRespaldo);
            }


            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdatePolitica(id, fileDto, fileDtoRespaldoParticipativo,fileDtoRespaldo, model, userId);
            return new DocumentoResponse { Ok = true };
        }
        [HttpPut("difusion/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutDifusion([FromRoute] int id, [FromForm] DifusionDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateDifusion(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }
        [HttpPut("procedimiento-papel/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutProcedimientoPapel([FromRoute] int id, [FromForm] ProcedimientoPapelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateProcedimientoPapel(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("procedimiento-residuo/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutProcedimientoResiduo([FromRoute] int id, [FromForm] ProcedimientoResiduoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateProcedimientoResiduo(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("procedimiento-residuo-sistema/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutProcedimientoResiduoSistema([FromRoute] int id, [FromForm] ProcedimientoResiduoSistemaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateProcedimientoResiduoSistema(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("procedimiento-baja-bienes/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutProcedimientoBajaBienes([FromRoute] int id, [FromForm] ProcedimientoBajaBienesDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateProcedimientoBajaBienes(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("procedimiento-compra-sustentable/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutProcedimientoCompraSustentable([FromRoute] int id, [FromForm] ProcedimientoCompraSustentableDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateProcedimientoCompraSustentable(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("procedimiento-reutilizacion-papel/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutProcedimientoReutilizacionPapel([FromRoute] int id, [FromForm] ProcReutilizacionPapelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateProcReutilizacionPapel(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("charlas/{id}")]
        public async Task<ActionResult<DocumentoResponse>> Putcharlas([FromRoute] int id, [FromForm] CharlasDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateCharlas(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("lista-integrantes-comite/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutListaIntegrantes([FromRoute] int id, [FromForm] ListaIntegrantesDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateListaIntegrantes(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("listado-colaboradores/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutListadoColaboradores([FromRoute] int id, [FromForm] ListadoColaboradorDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (model.EtapaSEV_docs == 1)
            {
                IFormFile file;
                FileDTO fileDto = new FileDTO();
                if (model.Adjunto is not null)
                {
                    var fileBytes = Convert.FromBase64String(model.Adjunto);
                    var filename = Path.GetFileName(model.AdjuntoPath);
                    MemoryStream stream = new MemoryStream(fileBytes);
                    file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                    fileDto = _documentosService.SaveFile(file);
                }

               
                await _documentosService.UpdateListadoColaboradores(id, model, userId, fileDto);
            }
            else if(model.EtapaSEV_docs == 2) 
            {
                await _documentosService.UpdateListadoColaboradores(id, model, userId);
            }



           
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("capacitados-mp/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutCapacitadosMP([FromRoute] int id, [FromForm] CapacitadosMPDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateCapacitadosMP(id, fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("pac-e3/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutPacE3([FromRoute] int id, [FromForm] PacE3DTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }
            IFormFile fileRespaldoCompromiso;
            FileDTO fileDtoRespaldoCompromiso = new FileDTO();
            if (model.AdjuntoRespaldoCompromiso is not null)
            {
                var fileBytesRespaldoCompromiso = Convert.FromBase64String(model.AdjuntoRespaldoCompromiso);
                var filenameRespaldoCompromiso = Path.GetFileName(model.AdjuntoRespaldoPathCompromiso);
                MemoryStream streamRespaldoCompromiso = new MemoryStream(fileBytesRespaldoCompromiso);
                fileRespaldoCompromiso = new FormFile(streamRespaldoCompromiso, 0, fileBytesRespaldoCompromiso.Length, filenameRespaldoCompromiso!, filenameRespaldoCompromiso!);
                fileDtoRespaldoCompromiso = _documentosService.SaveFile(fileRespaldoCompromiso);
            }


            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdatePacE3(id, fileDto,fileDtoRespaldoCompromiso, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpPut("resolucion-aprueba-plan/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutResolucionApruebaPlan([FromRoute] int id, [FromForm] ResolucionApruebaPlanDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }
           
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateResolucionApruebaPlan(id, fileDto,model, userId);
            return new DocumentoResponse { Ok = true };
        }


        [HttpPut("gestion-compra-sustentable/{id}")]
        public async Task<ActionResult<DocumentoResponse>> PutGestionCompraSustentable([FromRoute] int id, [FromForm] GestionCompraSustentableDTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            // IFormFile file;
            // FileDTO fileDto = new FileDTO();
            // if (model.Adjunto is not null)
            // {
            //     var fileBytes = Convert.FromBase64String(model.Adjunto);
            //     var filename = Path.GetFileName(model.AdjuntoPath);
            //     MemoryStream stream = new MemoryStream(fileBytes);
            //     file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
            //     fileDto = _documentosService.SaveFile(file);
            // }

            // IFormFile fileCompraSustentableAnt;
            // FileDTO fileDtoCompraSustentableAnt = new FileDTO();
            // if (model.AdjuntoCompraSustentableAnt is not null)
            // {
            //     var fileBytesCompraSustentableAnt = Convert.FromBase64String(model.AdjuntoCompraSustentableAnt);
            //     var filenameCompraSustentableAnt = Path.GetFileName(model.AdjuntoPathCompraSustentableAnt);
            //     MemoryStream streamCompraSustentableAnt = new MemoryStream(fileBytesCompraSustentableAnt);
            //     fileCompraSustentableAnt = new FormFile(streamCompraSustentableAnt, 0, fileBytesCompraSustentableAnt.Length, filenameCompraSustentableAnt!, filenameCompraSustentableAnt!);
            //     fileDtoCompraSustentableAnt = _documentosService.SaveFile(fileCompraSustentableAnt);
            // }

            // IFormFile fileCompraFuera;
            // FileDTO fileDtoCompraFuera = new FileDTO();
            // if (model.AdjuntoCompraFuera is not null)
            // {
            //     var fileBytesCompraFuera = Convert.FromBase64String(model.AdjuntoCompraFuera);
            //     var filenameCompraFuera = Path.GetFileName(model.AdjuntoPathCompraFuera);
            //     MemoryStream streamCompraFuera = new MemoryStream(fileBytesCompraFuera);
            //     fileCompraFuera = new FormFile(streamCompraFuera, 0, fileBytesCompraFuera.Length, filenameCompraFuera!, filenameCompraFuera!);
            //     fileDtoCompraFuera = _documentosService.SaveFile(fileCompraFuera);
            // }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await _documentosService.UpdateGestionCompraSustentable(id,model, userId);
            return new DocumentoResponse { Ok = true };
        }


        [HttpPost("informe-da")]
        public async Task<ActionResult<DocumentoResponse>> PostInformeDA([FromForm] InformeDADTO model)
        {
            if (!ModelState.IsValid)
            {
                return _documentosService.ReturnError("Los datos ingresados no son válidos");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, model.ServicioId))
            {
                return _documentosService.ReturnError("El usuario no pertenece al servicio");
            }

            FileDTO fileDto = new FileDTO();
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoPath);
                MemoryStream stream = new MemoryStream(fileBytes);
                IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                fileDto = _documentosService.SaveFile(file);
            }

            await _documentosService.SaveInformeDA(fileDto, model, userId);
            return new DocumentoResponse { Ok = true };
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DocumentoResponse>> DeleteDocumento([FromRoute] int id)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var response = await _documentosService.DeleteDocumento(id, userId);
            return response;
        }

    }
}
