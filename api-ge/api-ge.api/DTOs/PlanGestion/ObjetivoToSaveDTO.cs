using api_gestiona.Entities;
using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.PlanGestion
{
    public class ObjetivoToSaveDTO
    {
        public long DimensionBrechaId { get; set; }
        [Required]
        public string Titulo { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Descripcion { get; set; } = null!;
        public List<BrechaListDTO>? BrechasSelected { get; set; }
    }
}
