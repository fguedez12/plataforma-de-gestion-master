using GE.Models.ParametrosMedicionModels;
using GobEfi.Web.Models.CompraModels;
using GobEfi.Web.Models.MedidorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ConsumoModels
{
    public class ConsumoModel
    {
        public SubMenuConsumoModel SubMenu { get; set; }
        //public List<ParametrosMedicionModel> ParametrosMedicion { get; set; }

        public List<CompraTablaEnergetico> Compras { get; set; }
        

        public CompraModel NuevaCompra { get; set; }
        
    }
}
