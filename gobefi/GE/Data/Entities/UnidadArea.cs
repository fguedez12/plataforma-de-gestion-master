using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class UnidadArea
    {
        public long UnidadId { get; set; }
        public long AreaId { get; set; }
        public Unidad Unidad { get; set; }
        public Area Area { get; set; }
    }
}
