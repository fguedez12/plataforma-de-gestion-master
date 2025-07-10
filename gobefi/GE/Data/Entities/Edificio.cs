using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Edificio : BaseEntity
    {
        public string Direccion { get; set; }
        public string Numero { get; set; }
        public string Calle { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public double? Altitud { get; set; }
        public long? TipoEdificioId { get; set; }
        public long? ComunaId { get; set; }
        public long? TipoAgrupamientoId { get; set; }
        public long? EntornoId { get; set; }
        public long? InerciaTermicaId { get; set; }
        public long? FrontisId { get; set; }
        public Comuna Comuna { get; set; }
        public TipoEdificio TipoEdificio { get; set; }
        public ICollection<Division> Divisiones { get; set; }
        public int? OldId { get; set; }
        public virtual TipoAgrupamiento TipoAgrupamiento { get; set; }
        public virtual Entorno Entorno { get; set; }
        public virtual InerciaTermica InerciaTermica { get; set; }
        public ICollection<MedidorInteligenteEdificio> MedidorInteligenteEdificio { get; set; }
        public virtual Techo Techo { get; set; }
        public virtual Cimiento Cimiento { get; set; }
        public virtual ICollection<Piso> Pisos { get; set; }
        public int DpP1 { get; set; }
        public int DpP2 { get; set; }
        public int DpP3 { get; set; }
        public int DpP4 { get; set; }
    }
}
