using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.Shared.Entities
{
    public class MedidorInteligente :  BaseEntity
    {
        public long ChileMedidoId { get; set; }

        public ICollection<MedidorInteligenteEdificio> MedidoresInteligentesEdificios { get; set; }
        public ICollection<MedidorInteligenteServicio> MedidoresIntelligentesServicios { get; set; }

        public ICollection<MedidorInteligenteDivision> MedidorInteligenteDivisiones { get; set; }
    }
}
