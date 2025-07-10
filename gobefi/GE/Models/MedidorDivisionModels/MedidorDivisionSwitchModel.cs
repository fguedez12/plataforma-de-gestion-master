using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MedidorDivisionModels
{
    public class MedidorDivisionSwitchModel
    {
        public long MedidorId { get; set; }
        public long DivisionId { get; set; }
        public bool Activo { get; set; }
    }
}
