using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.InmuebleModels
{
    public class DireccionModel
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public long? RegionId { get; set; }
        public long? ProvinciaId { get; set; }
        public long? ComunaId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string DireccionCompleta { get; set; }
    }
}
