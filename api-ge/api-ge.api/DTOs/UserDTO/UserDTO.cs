namespace api_gestiona.DTOs.UserDTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string Sexo { get; set; }
        public long ServicioId { get; set; }
        public string Servicio { get; set; }
    }
}
