using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.UnidadesDTO
{
    public class InmuebleTopDTO
    {
        public long Id { get; set; }
        public int TipoInmueble { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Comuna { get; set; }
        public string Region { get; set; }
        public string Nombre { get; set; }
    }
}
