using GobEfi.Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Models
{
    public class MedidorModel
    {
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public long? ChileMedidoId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinimunList(1,ErrorMessage ="Debe ingresar al menos un id")]
        public List<long> EntidadIds { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string TipoEntidad { get; set; }
    }
}
