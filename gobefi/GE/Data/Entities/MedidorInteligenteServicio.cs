using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class MedidorInteligenteServicio : BaseEntity
    {
        public long MedidorInteligenteId { get; set; }
        public long ServicioId { get; set; }
        public MedidorInteligente MedidorInteligente { get; set; }
        public Servicio Servicio { get; set; }
    }
}
