using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Models.DTOs
{
    public class ReporteVehiculosDTO
    {
        public DateTime Fecha { get; set; }
        public List<VehiculosListaDTO> Vehiculos { get; set; }
    }
}
