using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class TipoUso
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public IEnumerable<Division> Divisiones { get; set; }
        public IEnumerable<Area> Areas { get; set; }
    }
}
