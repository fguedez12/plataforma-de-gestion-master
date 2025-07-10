using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class UsuarioUnidad
    {
        public string UsuarioId { get; set; }
        public long UnidadId { get; set; }
        public Usuario Usuario { get; set; }
        public Unidad Unidad { get; set; }
    }
}
