using System.ComponentModel.DataAnnotations;

namespace api_gestiona.Entities
{
    public class Programa : BaseEntity
    {
        public long ServicioId { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Descripcion { get; set; } = null!;
        [Required]
        public string AdjuntoUrl { get; set; } = null!;
        [Required]
        public string AdjuntoNombre { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;
    }
}
