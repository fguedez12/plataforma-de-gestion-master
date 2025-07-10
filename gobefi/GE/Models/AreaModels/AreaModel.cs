using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.AreaModels
{
    public class AreaModel
    {
        public long Id { get; set; }
        public long PisoId { get; set; }
        public string Nombre { get; set; }
        public long TipoUsoId { get; set; }
        public string NroRol { get; set; }
        public decimal Superficie { get; set; }
        public bool Active { get; set; }
        public List<UnidadAreaModel> UnidadesAreas { get; set; }
    }
}
