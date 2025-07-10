using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GobEfi.Web.Data.Entities
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

        // Campo para seguimiento según PRD v2.35 - Campo opcional
        [MaxLength(1000)]
        public string DescripcionTareaEjecutada { get; set; }

        // Propiedades de navegación
        [ForeignKey("DimensionBrechaId")]
        public virtual DimensionBrecha DimensionBrecha { get; set; }

        [ForeignKey("AccionId")]
        public virtual Accion Accion { get; set; }
    }
}