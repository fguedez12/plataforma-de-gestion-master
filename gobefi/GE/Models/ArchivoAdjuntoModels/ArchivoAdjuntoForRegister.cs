using GobEfi.Web.Core.Validation;
using Microsoft.AspNetCore.Http;

namespace GobEfi.Web.Models.ArchivoAdjuntoModels
{
    public class ArchivoAdjuntoForRegister
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long DivisionId { get; set; }
        public long TipoArchivoId { get; set; }
        public string Url { get; set; }
        public string ext { get; set; }
        public IFormFile Archivo { get; set; }

    }
}
