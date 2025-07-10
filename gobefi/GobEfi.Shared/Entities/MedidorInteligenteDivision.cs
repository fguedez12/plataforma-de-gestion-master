using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Shared.Entities
{
    public class MedidorInteligenteDivision : BaseEntity
    {
        public long MedidorInteligenteId { get; set; }

        public long DivisionId { get; set; }

        public MedidorInteligente MedidorInteligente { get; set; }
        public Division Division { get; set; }
    }
}
