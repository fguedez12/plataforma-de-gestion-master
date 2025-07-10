namespace api_gestiona.DTOs.PlanGestion
{
    public class PgaInfoDTO
    {
        public string? UserId { get; set; } = null!;
        public bool? PgaRevisionRed { get; set; }
        public string? PgaObservacionRed{ get; set; }
        public string? PgaRespuestaRed{ get; set; }
    }
}