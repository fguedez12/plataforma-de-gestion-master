namespace api_gestiona.Entities
{
    public class AccionServicio
    {
        public long AccionId { get; set; }
        public long ServicioId { get; set; }
        public Accion Accion { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;
    }
}
