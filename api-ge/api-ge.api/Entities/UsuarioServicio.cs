namespace api_gestiona.Entities
{
    public class UsuarioServicio
    {
        public string UsuarioId { get; set; } = null!;
        public Usuario Usuario { get; set; } = null!;

        public long ServicioId { get; set; }
        public Servicio Servicio { get; set; } = null!;
    }
}
