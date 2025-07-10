namespace api_gestiona.DTOs.PlanGestion
{
    public class ObjetivoListDTO
    {
        public long Id { get; set; }
        public long DimensionBrechaId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool TieneBrechaPorUnidad { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
