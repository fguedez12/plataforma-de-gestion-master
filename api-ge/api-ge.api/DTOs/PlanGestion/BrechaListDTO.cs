using api_gestiona.Entities;
using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.PlanGestion
{
    public class BrechaListDTO
    {
        public long Id { get; set; }
        public long DimensionBrechaId { get; set; }
        public int TipoBrecha { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public long? ObjetivoId { get; set; }
    }
}
