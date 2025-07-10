using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class ParametroMedicion
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long UnidadesMedidaId { get; set; }
        public UnidadMedida UnidadesMedida { get; set; }
    }
}