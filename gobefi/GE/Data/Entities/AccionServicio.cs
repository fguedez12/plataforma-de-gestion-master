namespace GobEfi.Web.Data.Entities
{
    public class AccionServicio
    {
        public long AccionId { get; set; }
        public long ServicioId { get; set; }
        public Accion Accion { get; set; }
        public Servicio Servicio { get; set; }
    }
}
