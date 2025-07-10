namespace api_gestiona.DTOs.Servicios
{
    public class ServicioDTO
    {
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Justificacion { get; set; }
        public bool RevisionRed { get; set; }
        public string? ComentarioRed { get; set; }
    }
}
