using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.Account
{
    public class UserCredentials
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [EmailAddress(ErrorMessage ="El email ingresado no es un email válido")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
