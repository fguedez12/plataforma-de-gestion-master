namespace GobEfi.Web.Data.Entities
{
    public class AccionUnidad
    {
        public long AccionId { get; set; }
        public long DivisionId { get; set; }
        public Accion Accion { get; set; }
        public Division Division { get; set; }
    }
}
