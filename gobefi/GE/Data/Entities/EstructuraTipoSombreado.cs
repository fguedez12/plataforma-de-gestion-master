using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EstructuraTipoSombreado
    {
        public long EstructuraId { get; set; }
        public long TipoSombreadoId { get; set; }
        public Estructura Estructura { get; set; }
        public TipoSombreado TipoSombreado { get; set; }
    }
}
