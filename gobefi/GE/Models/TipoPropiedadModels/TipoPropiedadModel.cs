using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.TipoPropiedadModels
{
    public class TipoPropiedadModel : BaseModel<long>
    {
        public string Nombre { get; set; }
    }
}
