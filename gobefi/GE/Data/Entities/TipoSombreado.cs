using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class TipoSombreado
    {
        public string Nombre { get ; set ; }
        public double FactorSombra { get ; set; }
        public long Id { get; set; }
        public ICollection<Muro> Muros { get; set; }
        public ICollection<EstructuraTipoSombreado> Estructuras { get; set; }
    }
}
