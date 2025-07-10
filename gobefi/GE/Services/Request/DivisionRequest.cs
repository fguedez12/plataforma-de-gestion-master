using GobEfi.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services.Request
{
    public class DivisionRequest : BaseRequest
    {
        public string Nombre { get; set; }
        public long InstitucionId { get; set; }
        public long ServicioId { get; set; }
        public bool Activo { get; set; } = true;
        public string Direccion { get; set; }
    }
}
