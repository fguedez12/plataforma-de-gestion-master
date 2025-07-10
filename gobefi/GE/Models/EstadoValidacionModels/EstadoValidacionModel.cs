using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EstadoValidacionModels
{
    public class EstadoValidacionModel :BaseModel<string>
    {
        public string Nombre { get; set; }
    }
}
