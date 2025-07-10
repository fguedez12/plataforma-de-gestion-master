using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.PlanGestion
{
    public class ProgramaToEditDTO
    {
        public long Id { get; set; }
        public long ServicioId { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string AdjuntoNombre { get; set; } = null!;
        public string? UserId { get; set; }
    }
}
