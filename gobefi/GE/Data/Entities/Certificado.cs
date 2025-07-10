using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Certificado : BaseEntity
    {
        public string Nombre { get; set; }
        public List<NotasCertificado> Notas { get; set; }
    }
}
