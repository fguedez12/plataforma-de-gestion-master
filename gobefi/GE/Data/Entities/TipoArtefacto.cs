using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class TipoArtefacto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public int? Orden { get; set; }
        public List<Artefacto> Artefactos { get; set; }
    }
}
