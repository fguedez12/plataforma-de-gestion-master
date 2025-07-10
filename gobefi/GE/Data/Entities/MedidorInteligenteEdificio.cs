using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class MedidorInteligenteEdificio : BaseEntity
    {
        public long MedidorInteligenteId { get; set; }
        public long EdificioId { get; set; }
        public MedidorInteligente MedidorInteligente { get; set; }
        public Edificio Edificio { get; set; }
    }
}
