using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EstructuraAislacion
    {
        public long EstructuraId { get; set; }
        public long AislacionId { get; set; }
        public Estructura Estructura { get; set; }
        public Aislacion Aislacion { get; set; }
    }
}
