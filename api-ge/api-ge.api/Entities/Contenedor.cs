using System.ComponentModel.DataAnnotations;

namespace api_gestiona.Entities
{
    public class Contenedor : BaseEntity
    {
        [Required]
        public string Nombre { get; set; }
        public string? Ubicacion { get; set; }
        
        public string? Propiedad { get; set; }
        public int? NRecipientes { get; set; }
        public string? TipoResiduo { get; set; }
        public float? Capacidad { get; set; }
        public long DivisionId { get; set; }
        public Division Division { get; set; }
    }
}
