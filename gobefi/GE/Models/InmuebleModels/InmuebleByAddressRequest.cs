using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.InmuebleModels
{
    public class InmuebleByAddressRequest
    {
        public string Calle { get; set; }
        public string Numero { get; set; }
        public long ComunaId { get; set; }
    }
}
