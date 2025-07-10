using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Models.DTOs
{
    public class FiltroVehiculosDTO
    {
        public string Tipo { get; set; }
        public string Marca { get; set; }
        public string Propulsion { get; set; }
        public int? Ministerio { get; set; }
    }
}
