using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string Email{ get; set; }
        public string Role { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public long ServicioId { get; set; }
    }
}
