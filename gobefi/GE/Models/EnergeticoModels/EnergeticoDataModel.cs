using GobEfi.Web.Models.UnidadMedidaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EnergeticoModels
{
    public class EnergeticoDataModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public List<UnidadMedidaModel> UnidadesMedida;

        public EnergeticoDataModel()
        {
            UnidadesMedida = new List<UnidadMedidaModel>();
        }
    }
}
