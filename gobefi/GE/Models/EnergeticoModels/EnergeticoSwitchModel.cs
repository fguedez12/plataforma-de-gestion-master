using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EnergeticoModels
{
    public class EnergeticoSwitchModel
    {
        public long EnergeticoId { get; set; }
        public bool State { get; set; }
        //public long UnidadMedidaId { get; set; }
        public long DivisionId { get; set; }

    }
}
