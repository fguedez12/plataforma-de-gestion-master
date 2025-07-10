namespace api_gestiona.DTOs.PlanGestion
{
    public class IndicadorListDTO
    {
        public long Id { get; set; }
        public long DimensionBrechaId { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
