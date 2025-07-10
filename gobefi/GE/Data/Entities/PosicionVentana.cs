using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class PosicionVentana : BaseEntity
    {
        public string Nombre { get; set; }
        public virtual ICollection<EstructuraPosicionVentana> Estructuras { get; set; }
    }
}
