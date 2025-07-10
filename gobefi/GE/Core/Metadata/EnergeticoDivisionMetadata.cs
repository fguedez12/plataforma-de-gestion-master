using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Core.Metadata
{
    public class EnergeticoDivisionMetadata
    {
        [Display(Name = "DivisionId")]
        public long DivisionId { get; set; }

        [Display(Name = "EnergeticoId")]
        public long EnergeticoId { get; set; }

        //[Display(Name = "Icono")]
        //private Division Division { get; set; }

        //[Display(Name = "Icono")]
        //private Energetico Energetico { get; set; }
    }
}
