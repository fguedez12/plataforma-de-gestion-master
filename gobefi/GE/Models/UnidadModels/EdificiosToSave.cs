using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.UnidadModels
{
    public class EdificiosToSave
    {
        public long Id { get; set; }
        public List<PisosToSave> Pisos { get; set; }
    }
}
