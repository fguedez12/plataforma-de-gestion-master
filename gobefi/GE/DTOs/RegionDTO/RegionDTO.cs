using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.RegionDTO
{
    public class RegionDTO : IDTOBase
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }
}
