using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class ArchivoAdjunto : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long DivisionId { get; set; }
        public long TipoArchivoId { get; set; }
        public string Url { get; set; }
        public TipoArchivo TipoArchivo { get; set; }
        public Division Division { get; set; }
        public Compra Compra { get; set; }
    }
}