using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.TipoUsoModels
{
    public class TipoUsoModel : BaseModel<long>
    {
        public string Nombre { get; set; }
    }
}
