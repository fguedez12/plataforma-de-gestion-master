using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Suelo : BaseEntity
    {
        public long? MaterialidadId { get; set; }
        public long? AislacionId { get; set; }
        public long?  PisoId { get; set; }
        public double Superficie { get; set; }
        public long? EspesorId { get; set; }
        public Materialidad Materialidad { get; set; }
        public Aislacion Aislacion { get; set; }
        public Piso Piso { get; set; }
        public Espesor Espesor { get; set; }

    }
}
