using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ServicioModels
{
    public class ServicioIndexModel : BasePagedModel<ServicioListModel>
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long InstitucionId { get; set; }
    }
}
