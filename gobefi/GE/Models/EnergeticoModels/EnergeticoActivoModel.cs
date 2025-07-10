using GobEfi.Web.Models.UnidadMedidaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EnergeticoModels
{
    public class EnergeticoActivoModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public bool TieneNumCliente { get; set; }
        public ICollection<UnidadMedidaModel> UnidadesMedida { get; set; }
    }
}
