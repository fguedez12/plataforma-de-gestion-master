using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CompraModels
{
    public class CompraParaValidarModel
    {
        public long Id { get; set; }
        public string InicioDeLectura { get; set; }
        public string Unidad { get; set; }
        public string Energetico { get; set; }
        public string NumCliente { get; set; }
        public string RevisadoPor { get; set; }
        public string Estado { get; set; }
        public string EstadoId { get; set; }

    }
}
