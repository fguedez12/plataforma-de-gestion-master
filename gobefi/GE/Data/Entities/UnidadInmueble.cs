using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class UnidadInmueble
    {
        public long InmuebleId { get; set; }
        public long UnidadId { get; set; }
        public Division Inmueble { get; set; }
        public Unidad Unidad { get; set; }
    }
}
