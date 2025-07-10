using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.PlanGestion
{
    public class TareaToEditDTO
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long DimensionBrechaId { get; set; }

        [Required]
        public long AccionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        [MaxLength(100)]
        public string Responsable { get; set; }

        [Required]
        [MaxLength(50)]
        public string EstadoAvance { get; set; }

        [MaxLength(500)]
        public string Observaciones { get; set; }

        // Nuevo campo agregado según Tarea #2 - Campo opcional
        [MaxLength(1000)]
        public string? DescripcionTareaEjecutada { get; set; }

        public string? UserId { get; set; }
    }
}