using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EstructuraMaterialidad
    {
        public long EstructuraId { get; set; }
        public long MaterialidadId { get; set; }
        public Estructura Estructura { get; set; }
        public Materialidad Materialidad { get; set; }
    }
}
