namespace api_gestiona.DTOs.PlanGestion
{
    public class ProgramaListDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
