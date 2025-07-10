using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.UnidadModels
{
    public class UnidadResponse
    {
        public string Message { get; set; }
        public List<UnidadModel> Unidades { get; set; }
        public bool Ok { get; set; }
    }
}
