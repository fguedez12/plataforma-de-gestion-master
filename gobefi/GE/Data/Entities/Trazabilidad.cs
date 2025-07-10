using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Trazabilidad : BaseEntity
    {
        public long DivisionId { get; set; }
        public string Observacion { get; set; }
        public string NombreTabla { get; set; }
        public string Accion { get; set; }
        public virtual Division Division { get; set; }
    }
}
