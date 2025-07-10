using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.PlanGestion
{
    public class IndicadorToEditDTO
    {
        public long Id { get; set; }
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
        public string? UserId { get; set; }
    }
}
