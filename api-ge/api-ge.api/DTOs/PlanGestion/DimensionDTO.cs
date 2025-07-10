namespace api_gestiona.DTOs.PlanGestion
{
    public class DimensionDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreNormalizado { get; set; } = null!;
        public string Target { get; set; } = null!;
        public bool IngresoObservacion { get; set; }
        public string? Observacion { get; set; }
        public bool IngresoObservacionObjetivos { get; set; }
        public string? ObservacionObjetivos { get; set; }
        public bool IngresoObservacionAcciones { get; set; }
        public string? ObservacionAcciones { get; set; }
        public bool IngresoObservacionIndicadores { get; set; }
        public string? ObservacionIndicadores { get; set; }
    }
}
