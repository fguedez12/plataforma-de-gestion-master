using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.AccountViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Telefono { get; set; }
        [Display(Name = "Ministerio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public long? MinisterioId { get; set; }
        [Display(Name = "Servicio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public long? ServicioId { get; set; }
        public string OtroServicio { get; set; }
    }
}
