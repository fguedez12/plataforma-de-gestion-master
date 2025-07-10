using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using GE.Core.Contracts.Services;
using GobEfi.Web.Controllers;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ReporteModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ApiBaseController
    {
        private readonly IReporteService _servReporte;
        private const string DISENIO_PASIVO = "Reporte Diseño Pasivo";

        public ReportesController(ApplicationDbContext context
        , UserManager<Usuario> manager
        , IHttpContextAccessor httpContextAccessor
        , IUsuarioService usuarioService
        , IReporteService servReporte) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _servReporte = servReporte;
        }

        [HttpGet("{servicioId}")]
        public IActionResult GetReportesBySerivicioId(long servicioId)
        {
            var usuarioActual = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            var reportes = _servReporte.GetByUser(usuarioActual.Id, servicioId);

            return Ok(reportes);
        }
        
        [HttpGet("{servicioId}/{reporteId}")]
        public async Task<IActionResult> ExportarExcel(long servicioId, long reporteId)
        {
            // string sFileName = @"demo.xlsx";

            // string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);


            ReporteModel configuracionReporte = _servReporte.Get(reporteId);
            string nombreCompleto = $"{configuracionReporte.Nombre}.{configuracionReporte.TipoArchivo.Extension}";

            MemoryStream memoryStream = null;


            if (string.IsNullOrEmpty(configuracionReporte.RutaDondeObtenerArchivo))
                
               

            if (configuracionReporte.Nombre == DISENIO_PASIVO)
            {
                    memoryStream = await _servReporte.DisenioPasivoReporte(servicioId);
                    return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreCompleto);

            }

            memoryStream = await _servReporte.ExportarExcel(servicioId, reporteId, nombreCompleto, _isAdmin);
            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreCompleto);

        }

       

    }
}