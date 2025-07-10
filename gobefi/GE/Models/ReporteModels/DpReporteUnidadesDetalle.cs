using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ReporteModels
{
    public class DpReporteUnidadesDetalle
    {
        public long IdUnidad { get; set; }
        public string DireccionUnidad { get; set; }
        public long IdEdificio { get; set; }
        public string Region { get; set; }
        public string Comuna { get; set; }
        public int AnioConstruccion { get; set; }
        public string TipoEdificio { get; set; }
        public string TipoPropiedad { get; set; }
        public decimal SuperficieTotal { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string TipoAgrupamiento { get; set; }
        public string Entorno { get; set; }
        public int NumeroPisos { get; set; }
        public string MurosIncompletos  { get; set; }
        public decimal SuperficiePromedio { get; set; }
        public decimal AlturaPromedio { get; set; }
        public decimal SuperficieTotalPisos { get; set; }
        public decimal AlturaTotal { get; set; }
        public decimal VolumenTotal { get; set; }
        public int NumeroMuros { get; set; }
        public decimal LargoMurosNorte { get; set; }
        public decimal LargoMurosEste { get; set; }
        public decimal LargoMurosSur { get; set; }
        public decimal LargoMurosOeste { get; set; }
        public string SolucionContructivaPiso { get; set; }
        public string TipoAislacionPiso { get; set; }
        public string EspesorAislacionPiso { get; set; }
        public string SolucionContructivaMuro { get; set; }
        public string AislacionMuro { get; set; }
        public string EspesorMuro { get; set; }
        public string SolucionConstructivaVentanas { get; set; }
        public string TipoCierreVentanas { get; set; }
        public string TipoMarcoVentanas { get; set; }
        public string PorcentajeVentana { get; set; }
        public string TipoPuertas { get; set; }
        public string TipoMarcoPuertas { get; set; }
        public string SuperficiePuertas { get; set; }
        public string SolucionConstructivaTechos { get; set; }
        public string TipoAislacionTechos { get; set; }
        public string EspesorAislacionTechos { get; set; }
        public string SolucionConstructivaCimiento { get; set; }
        public int NFotoEnvolventes { get; set; }
        public int NFotoDetalles { get; set; }
        public int NFotoProblemas { get; set; }
        public int NFotoArquitectura { get; set; }
        public int NFotoElevaciones { get; set; }
        public int NFotoEstructurales { get; set; }
        public int NFotoEspecialidad { get; set; }
    }
}
