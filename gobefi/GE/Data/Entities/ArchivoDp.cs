using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class ArchivoDp :BaseEntity
    {
        public string NombreArchivo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long Peso { get; set; }
        public string Seccion { get; set; }
        public string FileUrl { get; set; }
        public long DivisionId { get; set; }
        public virtual Division Division { get; set; }
    }
}
