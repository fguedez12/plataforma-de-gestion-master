using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ServicioModels
{
    public class ServicioSelectModel : BaseModel<long>
    {
        public string Nombre { get; set; }
    }
}
