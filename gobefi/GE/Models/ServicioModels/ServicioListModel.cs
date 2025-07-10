using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ServicioModels
{
    public class ServicioListModel : BaseModel<long>
    {
        public string Nombre { get; set; }
        public bool Selected { get; set; }
        public string InstitucionId { get; set; }
    }
}
