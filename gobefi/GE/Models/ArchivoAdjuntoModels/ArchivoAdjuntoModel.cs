using GobEfi.Web.Models.TipoArchivoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ArchivoAdjuntoModels
{
    public class ArchivoAdjuntoModel : BaseModel<long>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long DivisionId { get; set; }
        public long TipoArchivoId { get; set; }
        public string Url { get; set; }
        public TipoArchivoModel TipoArchivo { get; set; }
    }
}
