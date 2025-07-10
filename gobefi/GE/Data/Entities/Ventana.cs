using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Ventana : BaseEntity
    {
        public long? MaterialidadId { get; set; }
        public long? TipoCierreId { get; set; }
        public long? TipoMarcoId { get; set; }
        public long MuroId { get; set; }
        public long? PosicionVentanaId { get; set; }
        public double Ancho { get; set; }
        public double Largo { get; set; }
        public int Numero { get; set; }
        public double? Superficie { get; set; }
        public virtual Materialidad Materialidad { get; set; }
        public virtual Aislacion TipoCierre { get; set; }
        public virtual Aislacion TipoMarco { get; set; }
        public virtual Muro Muro { get; set; }
        public virtual PosicionVentana PosicionVentana { get; set; }
    }
}
