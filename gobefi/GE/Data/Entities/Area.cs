using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Area :  BaseEntity
    {
        public string Nombre { get; set; }
        public decimal Superficie { get; set; }
        public long TipoUsoId { get; set; }
        public long PisoId { get; set; }
        public string NroRol { get; set; }
        public TipoUso TipoUso { get; set; }
        public Piso Piso { get; set; }
        public List<UnidadArea> UnidadAreas { get; set; }

    }
}
