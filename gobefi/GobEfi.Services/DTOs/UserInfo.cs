using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.DTOs
{
    public class UserInfo
    {
        [Required(ErrorMessage ="Debe ingresar el nombre de usuario")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        public string Password { get; set; }
    }
}
