using GobEfi.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services.Request
{
    public class ServicioRequest : BaseRequest
    {
        public string Nombre { get; set; }
        public long InstitucionId { get; set; }
    }
}
