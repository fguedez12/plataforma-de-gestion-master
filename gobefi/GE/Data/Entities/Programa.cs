using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class Programa : BaseEntity
    {
        public long ServicioId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string AdjuntoUrl { get; set; }
        [Required]
        public string AdjuntoNombre { get; set; }
        public Servicio Servicio { get; set; }
    }
}
