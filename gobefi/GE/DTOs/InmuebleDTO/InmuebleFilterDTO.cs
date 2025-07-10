using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.InmuebleDTO
{
    public class InmuebleFilterDTO
    {
        public string Direccion { get; set; }
        public long? RegionId { get; set; }
        public long? ComunaId { get; set; }
    }
}
