using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs
{
    public class ObservacionInexistenciaDTO
    {
        [Required(ErrorMessage = "The observacion field is required.")]
        public string Observacion { get; set; }
    }
}