using Microsoft.AspNetCore.Identity;

namespace api_gestiona.Entities
{
    public class Usuario : IdentityUser
    {
        public long? SexoId { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public bool? Active { get; set; }
        public Sexo? Sexo { get; set; }
        public ICollection<UsuarioServicio>? UsuarioServicio { get; set; }

    }
}
