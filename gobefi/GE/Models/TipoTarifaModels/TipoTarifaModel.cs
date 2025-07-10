using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.TipoTarifaModels
{
    public class TipoTarifaModel : BaseModel<long>
    {
        public string Nombre { get; set; }
    }
}
