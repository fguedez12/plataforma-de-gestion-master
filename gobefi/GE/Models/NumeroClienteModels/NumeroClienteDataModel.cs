using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.NumeroClienteModels
{
    public class NumeroClienteDataModel
    {
        public long Id { get; set; }
        public string Numero { get; set; }
        public string NombreCliente { get; set; }
        public long EnergeticoId { get; set; }
    }
}
