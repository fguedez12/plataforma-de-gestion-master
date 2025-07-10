using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Muro : BaseEntity
    {
        public double  Azimut { get; set; }
        public double Distancia { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Orientacion { get; set; }
        public string TipoMuro { get; set; }
        public float Vanos { get; set; }
        public long PisoId { get; set; }
        public long? TipoSombreadoId { get; set; }
        public long? MaterialidadId { get; set; }
        public long? AislacionIntId { get; set; }
        public long? AislacionExtId { get; set; }
        public long? EspesorId { get; set; }
        public double? Largo { get; set; }
        public double? Altura { get; set; }
        public double? Superficie { get; set; }
        public int Order { get; set; }
        public double FachadaNorte { get; set; }
        public double FachadaEste { get; set; }
        public double FachadaSur { get; set; }
        public double FachadaOeste { get; set; }
        public virtual Piso Piso { get; set; }
        public virtual TipoSombreado TipoSombreado { get; set; }
        public virtual Materialidad Materialidad { get; set; }
        public virtual Aislacion AislacionInt { get; set; }
        public virtual Aislacion AislacionExt { get; set; }
        public Espesor Espesor { get; set; }
        public virtual ICollection<Ventana> Ventanas { get; set; }
        public virtual ICollection<Puerta> Puertas { get; set; }


    }
}
