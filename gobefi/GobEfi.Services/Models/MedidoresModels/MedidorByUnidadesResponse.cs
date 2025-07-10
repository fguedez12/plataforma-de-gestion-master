using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Models.MedidoresModels
{
    public class MedidorByUnidadesResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public List<UnidadModel> Unidades { get; set; }
    }
}
