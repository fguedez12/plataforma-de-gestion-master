using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class MedidorInteligente : BaseEntity
    {
        public long ChileMedidoId { get; set; }
        public ICollection<MedidorInteligenteEdificio> MedidorInteligenteEdificios { get; set; }
        public ICollection<MedidorInteligenteServicio> MedidorInteligenteServicios { get; set; }

        public ICollection<MedidorInteligenteDivision> MedidorInteligenteDivisiones { get; set; }
    }
}
