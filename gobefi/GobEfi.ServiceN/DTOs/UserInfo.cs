using System.ComponentModel.DataAnnotations;

namespace GobEfi.ServiceN.DTOs
{
    public class UserInfo
    {
        [Required(ErrorMessage = "Debe ingresar el nombre de usuario")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }
    }
}
