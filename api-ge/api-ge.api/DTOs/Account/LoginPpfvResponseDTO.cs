using api_gestiona.DTOs.Instituciones;
using api_gestiona.DTOs.Servicios;

namespace api_gestiona.DTOs.Account
{
    public class LoginPpfvResponseDTO
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public List<InstitucionListDTO> Instituciones { get; set; }
    }
}
