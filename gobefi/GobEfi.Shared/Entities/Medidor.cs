using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.Shared.Entities
{
    public class Medidor :BaseEntity
    {
        public string Numero { get; set; }
        public int Fases { get; set; }
        public bool Smart { get; set; }
        public bool Compartido { get; set; }
        public long NumeroClienteId { get; set; }
        public long? DivisionId { get; set; }
        public ICollection<MedidorDivision> MedidorDivision { get; set; }
    }
}
