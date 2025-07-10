using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EnergeticoDivision : BaseEntity
    {
        public long DivisionId { get;set; }
        public long EnergeticoId { get;set; }
        public long? NumeroClienteId { get; set; }

        public Division Division { get;set; }
        public Energetico Energetico { get;set; }
        public virtual NumeroCliente NumeroCliente { get; set; }
    }
}
