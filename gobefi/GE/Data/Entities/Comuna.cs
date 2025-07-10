using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Comuna
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long? ProvinciaId { get; set; }
        public long? RegionId { get; set; }
        public virtual ICollection<Division> Divisiones { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<EmpresaDistribuidoraComuna> EmpresaDistribuidoraComunas { get; set; }
        public List<Direccion> Direcciones { get; set; }

    }
}