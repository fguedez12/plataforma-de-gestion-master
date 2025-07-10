using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_gestiona.Entities
{
    [Table("PlanGestion_Tareas")]
    public class Tarea : BaseEntity
    {
        [Required]
        public long DimensionBrechaId { get; set; }

        [Required]
        public long AccionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Nombre { get; set; } = null!;

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        [Required]
        [MaxLength(100)]
        public string Responsable { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string EstadoAvance { get; set; } = null!;

        [MaxLength(500)]
        public string? Observaciones { get; set; }

        // Nuevo campo agregado según Tarea #2 - Campo opcional
        [MaxLength(1000)]
        public string? DescripcionTareaEjecutada { get; set; }

        // Propiedades de navegación
        [ForeignKey("DimensionBrechaId")]
        public virtual DimensionBrecha DimensionBrecha { get; set; } = null!;

        [ForeignKey("AccionId")]
        public virtual Accion Accion { get; set; } = null!;
    }
}