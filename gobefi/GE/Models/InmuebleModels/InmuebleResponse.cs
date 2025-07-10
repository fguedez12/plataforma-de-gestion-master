using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.InmuebleModels
{
    public class InmuebleResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public List<InmuebleModel> Inmuebles { get; set; }
    }
}
