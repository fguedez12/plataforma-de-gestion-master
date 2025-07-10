using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class UsuarioServicio
    {
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public long ServicioId { get; set; }
        public Servicio Servicio { get; set; }
    }
}
