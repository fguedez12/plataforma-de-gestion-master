using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.TipoArchivoModels
{
    public class TipoArchivoModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string MimeType { get; set; }
        public string Extension { get; set; }
        public string NombreCorto { get; set; }
    }
}
