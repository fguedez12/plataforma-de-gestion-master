namespace api_gestiona.DTOs.UserDTO
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public long ServicioId { get; set; }
    }
}
