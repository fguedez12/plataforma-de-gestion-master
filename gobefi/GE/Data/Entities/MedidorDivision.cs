using System.Collections;
using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class MedidorDivision : BaseEntity
    {

        public long DivisionId { get; set; }
        public long MedidorId { get; set; }

        public Division Division { get; set; }
        public Medidor Medidor { get; set; }

    }
}
