using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Models.UsuarioModels
{
    public class UsuarioCambioClaveModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string CambioEmail { get; set; }

        [DisplayName("Nueva clave")]
        [Required(ErrorMessage = "Debe ingresar una nueva clave.")]
        [DataType(DataType.Password)]
        public string NuevaClave { get; set; }

        [DisplayName("Confirmar clave")]
        [Required(ErrorMessage = "Debe confirmar la nueva clave.")]
        [DataType(DataType.Password)]
        [Compare("NuevaClave", ErrorMessage = "\"Nueva clave\" y \"Confirmar clave\" no coinciden.")]
        public string ConfirmarClave { get; set; }
    }
}
