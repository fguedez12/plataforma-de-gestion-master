using System.ComponentModel.DataAnnotations;

namespace api_gestiona.Entities
{
    public class Accion : BaseEntity
    {
        public long DimensionBrechaId { get; set; }
        public long ObjetivoId { get; set; }
        public long? MedidaId { get; set; }
        public string? OtraMedida { get; set; }
        public string? MedidaDescripcion { get; set; }
        [Required]
        public string Cobertura { get; set; } = null!;
        [Required]
        public string NivelMedida { get; set; } = null!;
        public string? GestorRespnsableId { get; set; }
        public string? ResponsableNombre { get; set; }
        public string? ResponsableEmail { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? AdjuntoNombre { get; set; }
        [Required]
        public string OtroServicio { get; set; } = null!;
        public long? PresupuestoIngenieria { get; set; }
        public bool? PresupuestoIngenieriaPedido { get; set; }
        public long? PresupuestoImplementacion { get; set; }
        public bool? PresupuestoImplementacionPedido { get; set; }
        public bool Piloto { get; set; }
        public int? ItemPresupuestario { get; set; }
        public int? Subtitulo { get; set; }
        public int? AsignacionPresupuestaria { get; set; }
        
        // Nuevos campos agregados según Tarea #2
        [MaxLength(100)]
        public string? CostoAsociado { get; set; }
        
        [MaxLength(50)]
        public string? EstadoAvance { get; set; }
        
        [MaxLength(1000)]
        public string? Observaciones { get; set; }
        
        public DimensionBrecha DimensionBrecha { get; set; } = null!;
        public Objetivo Objetivo { get; set; } = null!;
        public Medida Medida { get; set; } = null!;
        public ICollection<AccionUnidad>? AcionUnidades { get; set; }
        public ICollection<AccionServicio>? AccionServicios { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
