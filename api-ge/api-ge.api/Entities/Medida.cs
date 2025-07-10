using System.ComponentModel.DataAnnotations;

namespace api_gestiona.Entities
{
    public class Medida
    {
        public long Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        public long? DimensionbrechaId { get; set; }
        public int TipoMedida { get; set; }
        public string? Agrupacion { get; set; }
        public string? TipoDeMedida { get; set; }
        public DimensionBrecha? DimensionBrecha { get; set; }
    }
}
