namespace api_gestiona.DTOs.PlanGestion
{
    public class AccionListDTO
    {
        public long Id { get; set; }
        public long DimensionBrechaId { get; set; }
        public string MedidaDescripcion { get; set; } = null!;
        public string Medida { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        
        // Nuevos campos agregados según Tarea #2
        public string? CostoAsociado { get; set; }
        public string? EstadoAvance { get; set; }
        public string? Observaciones { get; set; }
        
        // Campos de presupuesto agregados según solicitud
        public int? Subtitulo { get; set; }
        public int? ItemPresupuestario { get; set; }
        public int? AsignacionPresupuestaria { get; set; }
        
        // Lista de tareas asociadas (opcional)
        public List<TareaListDTO>? Tareas { get; set; }
    }
}
