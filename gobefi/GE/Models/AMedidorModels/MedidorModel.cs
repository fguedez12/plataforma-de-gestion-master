using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.AMedidorModels
{
    public class MedidorModel
    {
        public long Id { get; set; }
        public string Numero { get; set; }
        public long DivisionId { get; set; }
        public string Division { get; set; }
        public long NumeroClienteId { get; set; }
        public string NumeroCliente { get; set; }
        public bool EsCompartido { get; set; }
        public bool EsInteligente { get; set; }
        public bool Active { get; set; }

    }
}
