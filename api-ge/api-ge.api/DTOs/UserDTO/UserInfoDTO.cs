using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.UserDTO
{
    public class UserInfoDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
