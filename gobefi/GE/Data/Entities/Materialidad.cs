using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Materialidad : BaseEntity
    {
        public string Nombre { get; set; }
        public virtual ICollection<EstructuraMaterialidad> Estructuras { get; set; }
        public virtual ICollection<Techo> Techos { get; set; }
        public virtual ICollection<Suelo> Suelos { get; set; }
        public virtual ICollection<Ventana> Ventanas { get; set; }
        public virtual ICollection<Cimiento> Cimientos { get; set; }
    }
}