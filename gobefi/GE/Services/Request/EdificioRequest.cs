using GobEfi.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services.Request
{
    public class EdificioRequest : BaseRequest
    {
        public long Id { get; set; }
        public string Direccion { get; set; }
        public long RegionId { get; set; }
        public long ComunaId { get; set; }
    }
}
