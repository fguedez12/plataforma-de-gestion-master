using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.UnidadModels
{
    public class InmueblesToSave
    {
        public long Id { get; set; }
        public List<EdificiosToSave> Edificios { get; set; }
    }
}
