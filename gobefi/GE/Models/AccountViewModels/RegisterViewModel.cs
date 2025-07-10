using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} carácteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repita contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        [Display(Name = "Código Postal")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Telefono { get; set; }
        [Display(Name ="Ministerio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public long? MinisterioId { get; set; }
        [Display(Name = "Servicio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public long? ServicioId { get; set; }
        public string OtroServicio { get; set; }
    }
}
