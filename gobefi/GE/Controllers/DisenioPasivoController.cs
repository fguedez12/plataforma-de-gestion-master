using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GobEfi.Business.Security;
using GobEfi.Business.Validaciones;
using GobEfi.Web.Core.Configuration;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.DisenioPasivoModels;
using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Models.TechoModels;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisenioPasivoController : ControllerBase
    {
        private readonly IDisenioPasivoService _serviDisenioPasivo;
        private readonly IConfiguration _configuration;
        private readonly ImpersonalizacionSettings _impersonalizacionSettings;
        private readonly IHostingEnvironment _hostingEnvironment;

        public DisenioPasivoController(IHostingEnvironment hostingEnvironment,IDisenioPasivoService servicioDisenioPasivo,IConfiguration configuration, ImpersonalizacionSettings impersonalizacionSettings)
        {
            _serviDisenioPasivo = servicioDisenioPasivo;
            _configuration = configuration;
            _impersonalizacionSettings = impersonalizacionSettings;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("division/{id}")]
        public async Task<ActionResult> GetDivision([FromRoute] long id)
        {
            try
            {
                return Ok(await _serviDisenioPasivo.GetDivision(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pasouno/{id}")]
        public async Task<ActionResult> GetPasoUnoData([FromRoute] long id) 
        {
            
            try
            {
                var response = await _serviDisenioPasivo.GetPasoUnoData(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); 
            }
           
        }

        [HttpGet("pasodos/{id}")]
        public async Task<ActionResult> GetPasoDosData([FromRoute] long id)
        {

            try
            {
                var response = await _serviDisenioPasivo.GetPasoDosData(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("pasotres/{id}")]
        public async Task<ActionResult> GetPasoTresData([FromRoute] long id)
        {

            try
            {
                var response = await _serviDisenioPasivo.GetPasoTresDataV2(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("pasocuatro/{id}")]
        public async Task<ActionResult> GetPasoCuatroData([FromRoute] long id)
        {

            try
            {
                var response = await _serviDisenioPasivo.GetPasoCuatroData(id);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // GET: api/DisenioPasivo/pasouno/5
        //[HttpGet("pasouno/{id}")]
        //public async Task<IActionResult> GetPasoUno([FromRoute] long id)
        //{
        //    try
        //    {
        //        PasoUnoForSave pasouno = await _serviDisenioPasivo.GetPasoUnoByDivisionId(id);

        //        return Ok(pasouno);
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}

        // PUT: api/DisenioPasivo/pasouno/5
        [HttpPut("pasouno/{id}")]
        public async Task<ActionResult<PasoUnoResponse>> PutPasoUno([FromRoute] long id, [FromBody] PasoUnoData model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response =  new PasoUnoResponse();
                response.Ok = true;
                response.Message = "OK";
                response.Id= await _serviDisenioPasivo.Update(id, model);
               

                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("pasotres/{id}")]
        public async Task<ActionResult<PasoTresResponse>> PostPasoTres([FromRoute] long id, [FromBody] PasoTresModel model)
        {
            try
            {
                var response = new PasoTresResponse() { 
                    Ok=true,
                    Message = "OK"
                };
                await _serviDisenioPasivo.PostPasoTresV2(id, model);
                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sueloslv2/{id}")]
        public async Task<ActionResult> PostSuelosLv2([FromRoute] long id, [FromBody] DivisionModel model)
        {
            try
            {
                await _serviDisenioPasivo.PostSuelosLv2(id, model);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("muroslv2/{id}")]
        public async Task<ActionResult> PostMurosLv2([FromRoute] long id, [FromBody] DivisionModel model)
        {
            try
            {
                await _serviDisenioPasivo.PostMurosLv2(id, model);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("ventanaslv2/{id}")]
        public async Task<ActionResult> PostVentanasLv2([FromRoute] long id, [FromBody] DivisionModel model)
        {
            try
            {
                await _serviDisenioPasivo.PostVentanasLv2(id, model);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("paso3level/{id}")]
        public async Task<ActionResult> PostPaso3level([FromRoute] long id)
        {
            try
            {
                await _serviDisenioPasivo.LevelPaso3(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("paso4archivos/{id}/{seccion}")]
        public async Task<ActionResult> PostPaso4Flie([FromRoute] long id, [FromRoute] string seccion) {
            try
            {
                var file = Request.Form.Files[0];
                var Level = "Fotos";
                var SubLevel = "Envolventes";
                switch (seccion)
                {
                    case "envolventes":
                        Level = "Fotos";
                        SubLevel = "Envolventes";
                        break;
                    case "detalles":
                        Level = "Fotos";
                        SubLevel = "Detalles";
                        break;
                    case "problemas":
                        Level = "Fotos";
                        SubLevel = "Problemas";
                        break;
                    case "arquitectura":
                        Level = "Planos";
                        SubLevel = "Arquitectura";
                        break;
                    case "elevaciones":
                        Level = "Planos";
                        SubLevel = "Elevaciones";
                        break;
                    case "estructurales":
                        Level = "Planos";
                        SubLevel = "Estructurales";
                        break;
                    case "especialidad":
                        Level = "Planos";
                        SubLevel = "Especialidad";
                        break;
                }

                var combineFolder = Path.Combine(Level, SubLevel);
                var folderName = Path.Combine(_configuration.GetValue<string>("DirectorioArchivos:DisenioPasivoFolder"), combineFolder);
                var pathToSave = Path.Combine(_configuration.GetValue<string>("DirectorioArchivos:DisenioPasivoServer"), folderName);
                   
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var ts = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    var fullPath = Path.Combine(pathToSave, ts + fileName);
                    var pubPath = (_configuration.GetValue<string>("DirectorioArchivos:DisenioPasivoPubFiles") +
                               _configuration.GetValue<string>("DirectorioArchivos:DisenioPasivoFolder") + '/' + Level + '/' + SubLevel + '/' + ts + fileName);
                    using (WindowsLogin wl = new WindowsLogin(_impersonalizacionSettings.Usuario, _impersonalizacionSettings.Dominio, _impersonalizacionSettings.Clave))
                    {
                        System.Security.Principal.WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                        {

                            if (!Directory.Exists(pathToSave))
                            {
                                Directory.CreateDirectory(pathToSave);
                            }
                           

                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                           
                        });
                    };
                    var newArchivo = new ArchivoDpModel()
                    {
                        NombreArchivo = fileName,
                        FileUrl = pubPath,
                        Peso = file.Length,
                        Seccion = SubLevel,
                        DivisionId = id,
                        Fecha = DateTime.Now
                    };
                    await _serviDisenioPasivo.PostFile(newArchivo);

                    return await Task.FromResult(Ok(newArchivo));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("downloadfiles")]
        public async Task<IActionResult> getFiles(string ids) {


            List<InMemoryFile> fileList = new List<InMemoryFile>();

            var arrIds = ids.Split('-');

            foreach (var id in arrIds)
            {

                //var file = await _serviDisenioPasivo.GetFileById(long.Parse(id));
                //var webClient = new WebClient();
                //byte[] imageBytes = webClient.DownloadData(file.FileUrl);


                var file = await _serviDisenioPasivo.GetFileById(long.Parse(id));
                var pub = _configuration.GetValue<string>("DirectorioArchivos:DisenioPasivoPubFiles");
                var nameFile = file.FileUrl.Replace(pub, "");
                nameFile = nameFile.Replace("/", "\\");
                var server = _configuration.GetValue<string>("DirectorioArchivos:DisenioPasivoServer");
                var pathToSave = Path.Combine(server, nameFile);

                using (WindowsLogin wl = new WindowsLogin(_impersonalizacionSettings.Usuario, _impersonalizacionSettings.Dominio, _impersonalizacionSettings.Clave))
                {
                    System.Security.Principal.WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                    {

                        var imageBytes = System.IO.File.ReadAllBytes(pathToSave);

                        var memFile = new InMemoryFile()
                        {
                            Content = imageBytes,
                            FileName = file.NombreArchivo
                        };

                        fileList.Add(memFile);

                    });
                };

               
             }

                var result = GetZipArchive(fileList);

                return File(result, "application/zip", "paso4.zip");

        }

        [HttpPost("deletefile/{id}")]
        public async Task<ActionResult<PasoCuatroResponse>> DeleteFile([FromRoute] long id)
        {
            try
            {
                var response = new PasoCuatroResponse()
                {
                    Ok = true,
                    Message = "OK"

                };
                await _serviDisenioPasivo.DeleteFile(id);
                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("updatefile")]
        public async Task<ActionResult<PasoCuatroResponse>> UpdateFile([FromBody] ArchivoDpModel model)
        {
            try
            {
                var response = new PasoCuatroResponse() { 
                    Ok = true,
                    Message = "OK"

                };
                await _serviDisenioPasivo.UpdateFile(model);
                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("setfrontis/{id}/{muroId}")]
        public async Task<ActionResult<PasoDosResponse>> SetFrontis([FromRoute] long id, [FromRoute] long muroId) {
            try
            {
                var response = new PasoDosResponse() {
                    Ok = true,
                    Message = "OK",
                    Id = await _serviDisenioPasivo.setFrontis(id, muroId)
            };

                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("pasodoscomplete/{id}")]
        public async Task<ActionResult> PasoDosComplete([FromRoute] long id)
        {
            try
            {
                await _serviDisenioPasivo.PasoDosComplete(id);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        public static byte[] GetZipArchive(List<InMemoryFile> files)
        {
            byte[] archiveFile;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open())
                            zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }

                archiveFile = archiveStream.ToArray();
            }

            return archiveFile;
        }


    }

    public class InMemoryFile
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
