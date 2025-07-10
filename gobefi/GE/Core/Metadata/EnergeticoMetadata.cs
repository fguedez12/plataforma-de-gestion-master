using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Core.Metadata
{
    public class EnergeticoMetadata
    {
        [Display(Name = "Icono")]
        public string Icono { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { set; get; }
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
    }
}
