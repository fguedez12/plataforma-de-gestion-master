using System.Collections.Generic;
using GobEfi.Web.Models.EnergeticoModels;

namespace GobEfi.Web.Models.ConsumoModels
{
    public class SubMenuConsumoModel
    {
         public List<EnergeticoActivoModel> EnergeticoSubMenu { get; set; }
         public List<int> AniosSubMenu { get;set; }
    }
}