using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required( ErrorMessage = "Su correo es obligatorio para cambiar clave.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required( ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar su clave.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseña con coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
