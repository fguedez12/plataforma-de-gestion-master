using System.ComponentModel.DataAnnotations;

namespace api_gestiona.Entities
{
    public class Indicador : BaseEntity
    {
        public long DimensionBrechaId { get; set; }
        public long ObjetivoId { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        public string? Numerador { get; set; }
        public string? Denominador { get; set; }
        public string? UnidadMedida { get; set; }
        public double? Valor { get; set; }
        public string? RespladoMonitoreo { get; set; }
        public DimensionBrecha DimensionBrecha { get; set; } = null!;
        public Objetivo Objetivo { get; set; } = null!;
    }
}
