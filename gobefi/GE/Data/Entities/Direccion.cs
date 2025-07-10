using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Direccion
    {
        public long Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string DireccionCompleta { get; set; }
        public long RegionId { get; set; }
        public long ProvinciaId { get; set; }
        public long ComunaId { get; set; }
        public Region Region { get; set; }
        public Provincia Provincia { get; set; }
        public Comuna Comuna { get; set; }
        public List<Division> Inmuebles { get; set; }
    }
}
