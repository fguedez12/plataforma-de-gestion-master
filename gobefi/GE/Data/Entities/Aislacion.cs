using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Aislacion : BaseEntity
    {
        public string Nombre { get; set; }
        public int SubNivel { get; set; }
        public virtual ICollection<EstructuraAislacion> Estructuras { get; set; }
        public virtual ICollection<Techo> Techos { get; set; }
        public virtual ICollection<Suelo> Suelos { get; set; }
        public virtual ICollection<Ventana> VentanasTipoMarco { get; set; }
        public virtual ICollection<Ventana> VentanasTipoCierre { get; set; }
    }
}
