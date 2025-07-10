using api_gestiona.Entities;
using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.PlanGestion
{
    public class ProgramaToSaveDTO
    {
        public long ServicioId { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        public string Adjunto { get; set; } = null!;
        [Required]
        public string AdjuntoNombre { get; set; } = null!;
        public string? UserId { get; set; }
    }
}
