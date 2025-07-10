using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.UnidadesDTO
{
    public class UnidadFilterDTO
    {
        public string Unidad { get; set; }
        public string userId { get; set; }
        public long? InstitucionId { get; set; }
        public long? ServicioId { get; set; }
        public long? RegionId { get; set; }

    }
}
