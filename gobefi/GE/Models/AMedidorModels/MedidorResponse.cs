using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.AMedidorModels
{
    public class MedidorResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public MedidorPaginModel MedidoresPorPagina { get; set; }
    }
}
