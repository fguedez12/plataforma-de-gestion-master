using System.Collections.Generic;

namespace GobEfi.Web.Models.DivisionModels
{

    public class DivisionesParaAsociar
    {
        public bool PermiteModificar { get; set; }

        public ICollection<DivisionesToAssociate> DivisionesList { get; set; }
    }

    public class DivisionesToAssociate
    {
        public long Id { get; set; }
        public string NombreEdificio { get; set; }
        public string NombreUnidad { get; set; }
        public string NombreRegion { get; set; }
        public string MideIntensidadConsumo { get; set; }
        public int NumGestores { get; set; }
        public string UnidadAsociada { get; set; }
    }
}
