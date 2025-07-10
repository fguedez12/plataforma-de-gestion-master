using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class ListaIntegrante : Documento
    {
        public ActaComite ActaComite { get; set; }
        public List<Integrante> Integrantes { get; set; }
    }
}
