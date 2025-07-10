using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class UnidadPiso
    {
        public long PisoId { get; set; }
        public long UnidadId { get; set; }
        public Piso Piso { get; set; }
        public Unidad Unidad { get; set; }
    }
}
