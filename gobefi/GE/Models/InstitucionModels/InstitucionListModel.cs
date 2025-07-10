using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.InstitucionModels
{
    public class InstitucionListModel : BaseModel<long>
    {
        public string Nombre { get; set; }
        public bool Selected { get; set; }
    }
}
