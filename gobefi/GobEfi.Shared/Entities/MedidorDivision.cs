using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.Shared.Entities
{
    public class MedidorDivision : BaseEntity
    {
        public long DivisionId { get; set; }
        public long MedidorId { get; set; }
        public Division Division { get; set; }
        public Medidor Medidor { get; set; }
    }
}
