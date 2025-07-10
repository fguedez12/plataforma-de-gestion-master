using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DisenioPasivoModels
{
    public class PasoUnoForSave
    {
        public long UnidadId { get; set; }
        public long TipoAgrupamientoId { get; set; }
        public long EntornoId { get; set; }
        public long? InerciaTermicaId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public bool PisosIguales { get; set; }
    }
}
