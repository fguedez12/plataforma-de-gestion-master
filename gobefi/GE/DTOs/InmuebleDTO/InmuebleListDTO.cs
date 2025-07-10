using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.InmuebleDTO
{
    public class InmuebleListDTO :IDTOBase
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string TipoInmueble { get; set; }
        public string Direccion { get; set; }

    }
}
