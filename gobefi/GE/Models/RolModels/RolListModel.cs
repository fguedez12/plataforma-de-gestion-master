using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.RolModels
{
    public class RolListModel : BaseModel<string>
    {
        public string Nombre { get; set; }
        public bool Selected { get; set; }
    }
}
