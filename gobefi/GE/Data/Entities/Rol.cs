using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Rol : IdentityRole
    {
        public string Nombre { get; set; }
        public int OldId { get; set; }
        public string DependeDelRoleId { get; set; }

        public virtual ICollection<UsuarioRol> UsuarioRoles { get; set; }
        public ICollection<ReporteRol> ReportesRol { get; set; }
        public ICollection<Permisos> Permisos { get; set; }
        public ICollection<PermisosBackEnd> PermisosBackEnd { get; set; }
    }
}
