using GobEfi.Web.Models.PisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DisenioPasivoModels
{
    public class PasoUnoData
    {
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public int? TipoAgrupamientoId { get; set; }
        public int? EntornoId { get; set; }
        public bool DpSt1 { get; set; }
        public bool DpSt2 { get; set; }
        public bool DpSt3 { get; set; }
        public bool DpSt4 { get; set; }
        public List<PisoPasoUnoModel> Pisos { get; set; }
    }
}
