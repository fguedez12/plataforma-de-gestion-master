using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class NumeroPiso : BaseEntity
    {
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int Nivel { get; set; }
        //public List<Piso> Pisos { get; set; }
    }
}
