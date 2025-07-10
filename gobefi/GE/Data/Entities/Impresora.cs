using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class Impresora : BaseEntity
    {
        [Required]
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string modelo { get; set; }
        public float? Potencia { get; set; }
        public int NumeroImpresiones { get; set; }
        public int BcnColor { get; set; }
        public int Caras { get; set; }
        [Required]
        public string TipoImpresora { get; set; }
        public long DivisionId { get; set; }
        public Division Division { get; set; }
    }
}
