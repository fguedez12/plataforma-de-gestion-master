using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Institucion : BaseEntity
    {
        public string Nombre { get; set; }
        public int OldId { get; set; }

        public ICollection<Servicio> Servicios { get; set; }
        public virtual ICollection<UsuarioInstitucion> UsuariosInstituciones { get; set; }
    }
}
