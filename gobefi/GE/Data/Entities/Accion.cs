using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class Accion : BaseEntity
    {
        public long DimensionBrechaId { get; set; }
        public long ObjetivoId { get; set; }
        public long? MedidaId { get; set; }
        public string OtraMedida { get; set; }
        public string MedidaDescripcion { get; set; }
        [Required]
        public string Cobertura { get; set; }
        [Required]
        public string NivelMedida { get; set; }
        public string GestorRespnsableId { get; set; }
        public string ResponsableNombre { get; set; }
        public string ResponsableEmail { get; set; }
        public string AdjuntoUrl { get; set; }
        public string AdjuntoNombre { get; set; }
        [Required]
        public string OtroServicio { get; set; }
        public long? PresupuestoIngenieria { get; set; }
        public bool? PresupuestoIngenieriaPedido { get; set; }
        public long? PresupuestoImplementacion { get; set; }
        public bool? PresupuestoImplementacionPedido { get; set; }
        public bool Piloto { get; set; }
        public int? ItemPresupuestario { get; set; }
        public int? Subtitulo { get; set; }
        public int? AsignacionPresupuestaria { get; set; }
        
        // Campos para seguimiento según PRD v2.35
        [MaxLength(100)]
        public string CostoAsociado { get; set; }
        
        [MaxLength(50)]
        public string EstadoAvance { get; set; }
        
        [MaxLength(1000)]
        public string Observaciones { get; set; }
        public DimensionBrecha DimensionBrecha { get; set; }
        public Objetivo Objetivo { get; set; }
        public Medida Medida{ get; set; }
        public ICollection<AccionUnidad> AcionUnidades { get; set; }
        public ICollection<AccionServicio> AccionServicios { get; set; }
        public ICollection<Tarea> Tareas { get; set; }
    }
}
