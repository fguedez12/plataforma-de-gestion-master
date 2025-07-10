using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class UsuarioInstitucion
    {
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public long InstitucionId { get; set; }
        public Institucion Institucion { get; set; }
    }
}
