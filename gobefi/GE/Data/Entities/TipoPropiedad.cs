using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class TipoPropiedad
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
    }
}