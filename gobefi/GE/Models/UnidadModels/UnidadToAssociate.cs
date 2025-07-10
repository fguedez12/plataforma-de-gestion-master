using System.Collections.Generic;

namespace GobEfi.Web.Models.UnidadModels
{
    public class UnidadParaAsociar
    {
        public bool PermiteModificar { get; set; }
        public ICollection<UnidadToAssociate> UnidadesList { get; set; }
    }
    public class UnidadToAssociate
    {
        public long Id { get; set; }
        public string NombreInmueble { get; set; }
        public string DireccionInmueble { get; set; }
        public string NombreUnidad { get; set; }
        public string NombreRegion { get; set; }
        public bool ReportaPMG { get; set; }
        public string MideIntensidadConsumo { get; set; }
        public string UnidadAsociada { get; set; }
    }
}
