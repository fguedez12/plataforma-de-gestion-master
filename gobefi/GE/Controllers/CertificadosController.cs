using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CertificadoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadosController : BaseController
    {
        private readonly ICertificadoService _service;

        public CertificadosController(ICertificadoService service, ApplicationDbContext context,
            IUsuarioService servUsuario,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor) : base(context, manager, httpContextAccessor, servUsuario)
        {
            _service = service;
        }

        [HttpGet("notas")]
        public async Task<NotaResponse> GetNotas(string filtroNombre,string filtroCorreo,int? filtroMinisterio,int? filtroServicio, int page= 1)
        {
            var response = new NotaResponse();
            try
            {
          
                    var list = _isAdmin ? await _service.ListNotas(true,page, filtroNombre, filtroCorreo, filtroMinisterio, filtroServicio) : await _service.ListNotas(false, page, filtroNombre, filtroCorreo, filtroMinisterio, filtroServicio) ; 
                    response.NotasPorPagina = list;
                    response.Ok = true;
                
                

                return response;
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
                return response;
            }

        }

        [HttpGet("notasbyuser")]
        public async Task<NotaResponse> NotasbyUser()
        {
            var response = new NotaResponse();
            try
            {
                var list = await _service.ListNotasByUser();
                response.Notas = list;
                response.Ok = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
                return response;
            }
          
         
        }

        [HttpGet]
        public async Task<ActionResult<CertificadoResponse>> GetCertificados() 
        {
            var response = new CertificadoResponse() { };
            try
            {
                var certificados = await _service.GetCertificados();
                response.Ok = true;
                response.Certificados = certificados;

                return response;


            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost("cargar/{id}")]
        public async Task<ActionResult<CargaResponse>> PostCargar([FromRoute] long id) 
        {
            try
            {
                var file = Request.Form.Files[0];

                var list = new List<NotaModel>();

                var validId = true;
                var validEmail = true;
                var validNota = true;
                var validSerie = true;
                var validFecha = true;
                var validDuracion = true;

                var countId = 0;
                var countEmail = 0;
                var countNota = 0;
                var countSerie = 0;
                var countFecha = 0;
                var countDuracion = 0;
                var countOk = 0;

                using (var steam = new MemoryStream())
                {
                    await file.CopyToAsync(steam);
                    using (var package = new ExcelPackage(steam)) 
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            //var usId = worksheet.Cells[row, 1].Value;
                            //if (usId==null)
                            //{
                            //    break;
                            //}

  

                            var xId = worksheet.Cells[row, 1].Value.ToString().Trim();
                            if (string.IsNullOrWhiteSpace(xId)) {
                                validId = false;
                                countId++;
                            }
                            var xEmail = worksheet.Cells[row, 2].Value.ToString().Trim();
                            if (string.IsNullOrWhiteSpace(xEmail))
                            {
                                validEmail = false;
                                countEmail++;
                            }

                            var xyNota = worksheet.Cells[row, 3].Value.ToString().Trim();

                            if (!double.TryParse(xyNota, out double xNota))
                            {
                                validNota = false;
                                countNota++;
                            }
                            var xSerie = worksheet.Cells[row, 4].Value.ToString().Trim();
                            if (string.IsNullOrWhiteSpace(xSerie))
                            {
                                validSerie = false;
                                countSerie++;
                            }

                            var xyFecha = worksheet.Cells[row, 5].Value.ToString().Trim();
                            if (!Int32.TryParse(xyFecha, out Int32 SerialDate))
                            {
                                validFecha = false;
                                countFecha++;
                            }

                            var xFecha = FromExcelSerialDate(SerialDate);


                            if (!Int32.TryParse(worksheet.Cells[row, 6].Value.ToString().Trim(), out Int32 xDuracion))
                            {
                                validDuracion = false;
                                countDuracion++;
                            }

                            var ticks = new DateTime(2016, 1, 1).Ticks;
                            var ans = DateTime.Now.Ticks - ticks;
                            var uniqueId = ans.ToString("x");
                            if (validId && validEmail && validNota && validSerie && validFecha && validDuracion)
                            {
                                countOk++;
                                list.Add(new NotaModel
                                {
                                    UsuarioId = xId,
                                    Email = xEmail,
                                    NotaFinal = xNota,
                                    NumeroSerie = xSerie,
                                    FechaEntrega = xFecha,
                                    Duracion = xDuracion,
                                    CertificadoId = id,
                                    Codigo = uniqueId
                                });
                            }
                            
                        }
                    }

                }

                await _service.PostNotas(list);
                var response = new CargaResponse() 
                {
                    CountDuracion = countDuracion,
                    CountEmail = countEmail,
                    CountFecha = countFecha,
                    CountId=countId,
                    CountNota = countNota,
                    CountSerie=countSerie,
                    CountOk  = countOk
                };
                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        private static DateTime FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59) SerialDate -= 1; //Excel/Lotus 2/29/1900 bug   
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }
    }
}
