using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Medidor : BaseEntity
    {
        public string Numero { get; set; }
        public int Fases { get; set; }
        public bool Smart { get; set; }
        public bool Compartido { get; set; }
        public bool? Factura { get; set; }
        public long NumeroClienteId { get; set; }
        public long? DivisionId { get; set; }
        public bool MedidorConsumo { get; set; }
        public bool Chilemedido { get; set; }
        public int? DeviceId { get; set; }
        public NumeroCliente NumeroCliente { get; set; }
        public ICollection<CompraMedidor> CompraMedidor { get;set; }
        public ICollection<MedidorDivision> MedidorDivision { get; set; }
        public Division Division { get; set; }
        //public long OldId { get; set; }
    }
}
