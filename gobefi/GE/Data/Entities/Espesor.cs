using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Espesor
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public List<Suelo> Suelos { get; set; }
        public List<Muro> Muros { get; set; }
        public List<Techo> Techos { get; set; }
    }
}
