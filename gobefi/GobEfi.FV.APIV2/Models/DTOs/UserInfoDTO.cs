using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Models.DTOs
{
    public class UserInfoDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public long ServicioId { get; set; }
    }
}
