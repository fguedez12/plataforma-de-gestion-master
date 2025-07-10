using GobEfi.Web.Models.AislacionesModels;
using GobEfi.Web.Models.MaterialidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EstructuraModels
{
    public class EstructuraModel
    {
        public string Nombre { get; set; }
        public List<EstructuraMaterialidadModel> Materialidades { get; set; }
        public List<EstructuraAislacionModel> Aislaciones { get; set; }
        public List<EstructuraTipoSombreadoModel> TiposSombreado { get; set; }
        public List<EstructuraPosicionVentanaModel> PosicionVentanas { get; set; }
    }
}
