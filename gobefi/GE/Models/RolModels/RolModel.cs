using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.RolModels
{
    public class RolModel : BaseModel<string>
    {
        public string Nombre { get; set; }
        public string NormalizedName { get; set; }
    }
}
