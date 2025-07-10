using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Estructura : BaseEntity
    {
        public string Nombre { get; set; }
        public virtual ICollection<EstructuraMaterialidad> Materialidades { get; set; }
        public virtual ICollection<EstructuraAislacion> Aislaciones { get; set; }
        public virtual ICollection<EstructuraTipoSombreado> TiposSombreado { get; set; }
        public virtual ICollection<EstructuraPosicionVentana> PosicionVentanas { get; set; }
        
    }
}
