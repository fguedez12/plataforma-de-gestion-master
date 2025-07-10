using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EmpresaDistribuidora : BaseEntity
    {
        public string Nombre { get; set; }
        public long EnergeticoId { get; set; }
        public long OldId { get; set; }
        public string RUT { get; set; }

        public Energetico Energetico { get; set; }
        public ICollection<EmpresaDistribuidoraComuna> EmpresaDistribuidoraComunas { get; set; }

    }
}
