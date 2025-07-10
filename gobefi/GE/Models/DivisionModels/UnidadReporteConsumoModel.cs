using GobEfi.Web.DTOs.UnidadesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DivisionModels
{
    public class UnidadReporteConsumoModel
    {
        public int Total { get; set; }
        public int TotalPMG { get; set; }
        public List<UnidadReporteConsumoDTO> Unidades { get; set; }
    }
}
