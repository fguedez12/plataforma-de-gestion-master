using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.VentanaModels
{
    public class VentanaModel
    {
        public long Id { get; set; }
        public long MaterialidadId { get; set; }
        public long TipoCierreId { get; set; }
        public long TipoMarcoId { get; set; }
        public long? PosicionVentanaId { get; set; }
        public long MuroId { get; set; }
        public double Ancho { get; set; }
        public double Largo { get; set; }
        public int Numero { get; set; }
        public double Superficie { get; set; }
        public Muro Muro { get; set; }
    }
}
