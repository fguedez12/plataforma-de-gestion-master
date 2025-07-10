using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EstructuraPosicionVentana
    {
        public long EstructuraId { get; set; }
        public long PosicionVentanaId { get; set; }
        public virtual Estructura Estructura { get; set; }
        public virtual PosicionVentana PosicionVentana { get; set; }
    }
}
