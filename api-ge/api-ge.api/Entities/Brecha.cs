using System.ComponentModel.DataAnnotations;

namespace api_gestiona.Entities
{
    public class Brecha : BaseEntity
    {
        public long ServicioId { get; set; }
        [Required]
        public string Descripcion { get; set; } = null!;
        public long DimensionBrechaId { get; set; }
        public int TipoBrecha { get; set; }
        [Required]
        public string Titulo { get; set; } = null!;
        public int Priorizacion { get; set; }
        public long? ObjetivoId { get; set; }
        public DimensionBrecha DimensionBrecha { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;
        public ICollection<BrechaUnidad>? BrechaUnidades { get; set; }
        public Objetivo? Objetivo { get; set; }
    }
}
