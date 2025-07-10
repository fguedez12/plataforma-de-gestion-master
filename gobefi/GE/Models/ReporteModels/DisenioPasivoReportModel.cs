using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ReporteModels
{
    public class DisenioPasivoReportModel
    {
        public int NumeroEdificios { get; set; }
        public string Servicio { get; set; }
        public string Fecha { get; set; }
        public List<DpReporteEdificioModel> Edificios { get; set; }
        public List<DpReporteUnidadesDetalle> Unidades { get; set; }
    }
}
