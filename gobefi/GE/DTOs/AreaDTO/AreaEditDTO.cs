using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.AreaDTO
{
    public class AreaEditDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public decimal Superficie { get; set; }
        public long TipoUsoId { get; set; }
        public long PisoId { get; set; }
        public string NroRol { get; set; }
    }
}
