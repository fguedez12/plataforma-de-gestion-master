using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GobEfi.Business.Security;
using GobEfi.Web.Core.Configuration;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.ArchivoAdjuntoModels;
using GobEfi.Web.Models.TipoArchivoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoAdjuntoController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly IArchivoAdjuntoService _servArchivoAdjunto;
        private readonly DirectorioArchivos _directorios;
        private readonly ImpersonalizacionSettings _impersonalizacionSettings;
        private readonly ILogger _logger;

        public ArchivoAdjuntoController(IArchivoAdjuntoService servArchivoAdjunto, ILoggerFactory loggerFactory, DirectorioArchivos directorios, ImpersonalizacionSettings impersonalizacionSettings)
        {
            _servArchivoAdjunto = servArchivoAdjunto;
            _logger = loggerFactory.CreateLogger<ArchivoAdjuntoController>();
            _directorios = directorios;
            _impersonalizacionSettings = impersonalizacionSettings;
        }


        [HttpGet("GetExtPermitidasFactura")]
        public async Task<IActionResult> GetExtPermitidasFactura()
        {
            ICollection<TipoArchivoModel> extPermitidas = await _servArchivoAdjunto.GetExtPermitidasFacturaAsync();

            return Ok(extPermitidas);
        }

        [AllowAnonymous]
        [HttpGet("getByFacturaId/{facturaId}")]
        public async Task<ActionResult> GetByCompraId([FromRoute] long facturaId)
        {
            ArchivoAdjuntoModel factura = await _servArchivoAdjunto.GetByFacturaId(facturaId);


            byte[] FileBytes = null;

            try
            {
                using (WindowsLogin wl = new WindowsLogin(_impersonalizacionSettings.Usuario, _impersonalizacionSettings.Dominio, _impersonalizacionSettings.Clave))
                {
                    System.Security.Principal.WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                    {
                        FileBytes = System.IO.File.ReadAllBytes(factura.Url);
                    });
                };
            }
            catch (Exception)
            {
                return BadRequest("Archivo no existe.");
            }



            return File(FileBytes, factura.TipoArchivo.MimeType, factura.Nombre);
        }


        // api/archivoAdjunto/division/2
        [HttpPost("division/{divisionId}")]
        public async Task<IActionResult> AddArchivoAdjuntoForCompras([FromForm] ArchivoAdjuntoForRegister archivoAdjunto, int divisionId)
        {
            if (archivoAdjunto.Archivo == null)
            {
                return BadRequest(new { Factura = "Debe ingresar un archivo"});
            }

            var err = ValidarArchivoCompra(archivoAdjunto.Archivo);
            foreach (var item in err)
                ModelState.AddModelError(item.Key, item.Value);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            string tokenName = Guid.NewGuid().ToString();
            string nombreArchivo = $"{_directorios.Facturas}\\{tokenName}";

            // guardar en base de datos
            archivoAdjunto.Nombre = archivoAdjunto.Archivo.FileName;
            archivoAdjunto.Url = nombreArchivo;
            archivoAdjunto.DivisionId = divisionId;
            archivoAdjunto.ext = Path.GetExtension(archivoAdjunto.Archivo.FileName);

            archivoAdjunto.Id = await _servArchivoAdjunto.Add(archivoAdjunto);

            if (archivoAdjunto.Id <= 0)
                return BadRequest(new { Error = "Error al ingresar compra a la base de datos" });

            using (WindowsLogin wl = new WindowsLogin(_impersonalizacionSettings.Usuario, _impersonalizacionSettings.Dominio, _impersonalizacionSettings.Clave))
            {
                System.Security.Principal.WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {
                    using (var stream = new FileStream(nombreArchivo, FileMode.Create))
                    {
                        archivoAdjunto.Archivo.CopyTo(stream);
                    };
                });
            };


            return Ok(new { success = true, newId = archivoAdjunto.Id });
        }

        private Dictionary<string, string> ValidarArchivoCompra(IFormFile archivo)
        {
            var errores = new Dictionary<string, string>();

            if (archivo.Length > 4000000) // 4MB
            {
                errores.Add("factura", "Tamaño del archivo supera los 4 MB.");
            }


            if (!_servArchivoAdjunto.ValidaArchivoParaCompra(Path.GetExtension(archivo.FileName.ToLower())))
            {
                errores.Add("factura", "Tipo de archivo no valido.");
            }

            return errores;
        }

        // api/archivoAdjunto/5/division/2
        [HttpPut("{id}/division/{divisionId}")]
        public IActionResult PutArchivoAdjuntoForCompra([FromRoute] long divisionId, [FromRoute] long id, [FromForm] ArchivoAdjuntoForEdit archivoAdjuntoForEdit)
        {
            if (archivoAdjuntoForEdit.Archivo == null)
            {
                return NoContent();
            }

            var err = ValidarArchivoCompra(archivoAdjuntoForEdit.Archivo);
            foreach (var item in err)
                ModelState.AddModelError(item.Key, item.Value);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != archivoAdjuntoForEdit.Id)
            {
                return BadRequest();
            }
            
            string tokenName = Guid.NewGuid().ToString();

            string rutaDestino = Path.Combine($"{_directorios.Facturas}", tokenName);

            var archivoOriginal = _servArchivoAdjunto.Get(archivoAdjuntoForEdit.Id);

            // guardar en base de datos
            archivoAdjuntoForEdit.Nombre = archivoAdjuntoForEdit.Archivo.FileName;
            archivoAdjuntoForEdit.Url = rutaDestino;
            archivoAdjuntoForEdit.DivisionId = divisionId;

            var result = _servArchivoAdjunto.Update(archivoAdjuntoForEdit);

            if (result != 1)
                return BadRequest(new { Error = "Error al actualizar archivo en base de datos." });

            using (WindowsLogin wl = new WindowsLogin(_impersonalizacionSettings.Usuario, _impersonalizacionSettings.Dominio, _impersonalizacionSettings.Clave))
            {
                System.Security.Principal.WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                {
                    if (System.IO.File.Exists(archivoOriginal.Url))
                        System.IO.File.Delete(archivoOriginal.Url);

                    using (var stream = new FileStream(rutaDestino, FileMode.Create))
                    {
                        archivoAdjuntoForEdit.Archivo.CopyTo(stream);
                    };
                });
            };

            return NoContent();
        }


        // DELETE: api/archivoAdjunto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}