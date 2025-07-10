using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core
{
    public enum EnumTipoMensaje
    {
        Success,
        Warning,
        Error
    }

    public enum EnumTiene : int
    {
        [Description(Constants.Titles.SELECCIONE)]
        SinOpcion = 0,
        [Description("SI")]
        Si = 1,
        [Description("NO")]
        No = 2
    }

    public enum TipoNivel
    {
        [Display(Name ="Piso bajo nivel")]
        BajoNivel = 1,
        [Display(Name = "Piso sobre nivel")]
        SobreNivel = 2
    }

    public enum TipoMuro
    {
        [Display(Name = "Muro interno")]
        Interno = 1,
        [Display(Name = "Muro externo")]
        Externo = 2
    }
}
