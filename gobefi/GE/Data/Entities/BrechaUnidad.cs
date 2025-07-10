namespace GobEfi.Web.Data.Entities
{
    public class BrechaUnidad
    {
        public long BrechaId { get; set; }
        public long DivisionId { get; set; }
        public Brecha Brecha { get; set; }
        public Division Division { get; set; }
    }
}
