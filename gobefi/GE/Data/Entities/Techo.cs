using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Techo : BaseEntity
    {
        public long? MaterialidadId { get; set; }
        public long? AislacionId { get; set; }
        public double? Superficie { get; set; }
        public long EdificioId { get; set; }
        public long? EspesorId { get; set; }
        public virtual Materialidad Materialidad { get; set; }
        public virtual Aislacion Aislacion { get; set; }
        public virtual Edificio Edificio { get; set; }
        public Espesor Espesor { get; set; }
    }
}
