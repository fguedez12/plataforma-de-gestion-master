using GobEfi.Web.Models.CimientoModels;
using GobEfi.Web.Models.MuroModels;
using GobEfi.Web.Models.PuertaModels;
using GobEfi.Web.Models.SueloModels;
using GobEfi.Web.Models.TechoModels;
using GobEfi.Web.Models.VentanaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DisenioPasivoModels
{
    public class PasoTresModel
    {
        public PisoPasoTresModel Piso { get; set; }
        public MuroPasoTresModel Muro { get; set; }
        public VentanasPasoTres Ventanas { get; set; }
        public PuertasPasoTres Puertas { get; set; }
        public TechoPasoTres Techo { get; set; }
        public CimientoPasoTres Cimiento { get; set; }
    }
}
