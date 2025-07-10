using api_gestiona.Entities;
using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.Contenedores
{
    public class ContenedorDTO
    {
        public long Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Ubicacion { get; set; }
        [Required]
        public string Propiedad { get; set; }
        public int? NRecipientes { get; set; }
        [Required]
        public string TipoResiduo { get; set; }
        public float? Capacidad { get; set; }
        public long DivisionId { get; set; }
    }
}
