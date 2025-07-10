using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CompraMedidorModels
{
    public class CompraMedidorForValidate
    {
        public long Id { get; set; }
        public double Consumo { get; set; }
        public string Medidor { get; set; }
        public long MedidorId { get; set; }
        public long CompraId { get; set; }
        public string ParametroMedicion { get; set; }
        public string UnidadMedida { get; set; }
        public string Abrv { get; set; }
    }
}
