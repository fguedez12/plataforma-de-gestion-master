using GobEfi.Web.Models.CimientoModels;
using GobEfi.Web.Models.EntornoModels;
using GobEfi.Web.Models.TechoModels;
using GobEfi.Web.Models.TipoAgrupamientoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EdificioModels
{
    public class EdificioDPModel
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public long? TipoAgrupamientoId { get; set; }
        public long? EntornoId { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public long? FrontisId { get; set; }
        public TipoAgrupamientoModel TipoAgrupamiento { get; set; }
        public EntornoModel Entorno {get; set; }
        public TechoModel Techo { get; set; }
        public CimientoModel Cimiento { get; set; }
    }
}
