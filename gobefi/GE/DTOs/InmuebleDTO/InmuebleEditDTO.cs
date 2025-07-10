using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.InmuebleDTO
{
    public class InmuebleEditDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public double Superficie { get; set; }
        public int AnyoConstruccion { get; set; }
        public long TipoUsoId { get; set; }
        public long TipoAdministracionId { get; set; }
        public long? AdministracionServicioId { get; set; }
        public string NroRol { get; set; }
    }
}
