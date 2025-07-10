using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EmpresaDistribuidoraComuna : BaseEntity
    {
        public long EmpresaDistribuidoraId { get; set; }
        public long ComunaId { get; set; }

        public EmpresaDistribuidora EmpresaDistribuidora { get; set; }
        public Comuna Comuna { get; set; }
    }
}
