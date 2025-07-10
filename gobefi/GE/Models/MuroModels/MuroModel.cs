using GobEfi.Web.Models.AislacionesModels;
using GobEfi.Web.Models.MaterialidadModels;
using GobEfi.Web.Models.PuertaModels;
using GobEfi.Web.Models.TipoSombreadoModels;
using GobEfi.Web.Models.VentanaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MuroModels
{
    public class MuroModel 
    {
        public long Id { get; set; }
        public float Vanos { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public float Distance { get; set; }
        public float Bearing { get; set; }
        public string Tipo { get; set; }
        public string Orientation { get; set; }
        public long? TipoSombreadoId { get; set; }
        public double? Largo { get; set; }
        public double? Altura { get; set; }
        public double? Superficie { get; set; }
        public long? MaterialidadId { get; set; }
        public long? AislacionIntId { get; set; }
        public long? AislacionExtId { get; set; }
        public long?  EspesorId { get; set; }
        public int Order { get; set; }
        public TipoSombreadoModel TipoSombreado { get; set; }
        public MaterialidadModel Materialidad { get; set; }
        public AislacionModel AislacionInt { get; set; }
        public AislacionModel AislacionExt { get; set; }
        public List<VentanaModel> Ventanas { get; set; }
        public List<PuertaModel> Puertas { get; set; }
    }
}
