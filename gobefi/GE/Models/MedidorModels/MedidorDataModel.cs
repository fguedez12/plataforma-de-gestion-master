using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MedidorModels
{
    public class MedidorDataModel
    {
        public long Id { get; set; }
        public string Numero { get; set; }
        public long NumeroClienteId { get; set; }
    }
}
