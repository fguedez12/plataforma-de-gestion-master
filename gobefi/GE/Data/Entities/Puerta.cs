using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Puerta : BaseEntity
    {
        public long? MaterialidadId { get; set; }
        public long? TipoMarcoId { get; set; }
        public double? Superficie { get; set; }
        public long MuroId { get; set; }
        public virtual Materialidad Materialidad { get; set; }
        public virtual Aislacion TipoMarco { get; set; }
        public virtual Muro Muro { get; set; }


    }
}
