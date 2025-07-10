namespace api_gestiona.Entities
{
    public class AccionUnidad
    {
        public long AccionId { get; set; }
        public long DivisionId { get; set; }
        public Accion Accion { get; set; } = null!;
        public Division Division { get; set; } = null!;
    }
}
