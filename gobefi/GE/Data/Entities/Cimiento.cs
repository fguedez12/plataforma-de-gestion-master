using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Cimiento : BaseEntity
    {
        public long? MaterialidadId { get; set; }
        public long EdificioId { get; set; }
        public virtual Materialidad Materialida { get; set; }
        public virtual Edificio Edificio { get; set; }
    }
}
