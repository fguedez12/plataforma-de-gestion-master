using System.ComponentModel.DataAnnotations;

namespace api_gestiona.Entities
{
    public class Objetivo : BaseEntity
    {
        public long DimensionBrechaId { get; set; }
        [Required]
        public string Titulo { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Descripcion { get; set; } = null!;
        public DimensionBrecha DimensionBrecha { get; set; } = null!;
        public ICollection<Brecha>? Brechas { get; set; }
        public ICollection<Accion>? Acciones { get; set; }
        public ICollection<Indicador>? Indicadores { get; set; }
    }
}
