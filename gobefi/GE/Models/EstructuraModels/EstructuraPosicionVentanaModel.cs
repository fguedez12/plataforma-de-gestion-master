using GobEfi.Web.Models.PosicionVentanaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EstructuraModels
{
    public class EstructuraPosicionVentanaModel
    {
        public long EstructuraId { get; set; }
        public long PosicionVentanaId { get; set; }
        public EstructuraModel Estructura { get; set; }
        public PosicionVentanaModel PosicionVentana { get; set; }
    }
}
