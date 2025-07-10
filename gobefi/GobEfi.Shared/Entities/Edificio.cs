using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Shared.Entities
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

        public Comuna Comuna { get; set; }
        public TipoEdificio TipoEdificio { get; set; }

        public int? OldId { get; set; }

        public ICollection<MedidorInteligenteEdificio> MedidoresInteligentesEdificios { get; set; }

    }
}
