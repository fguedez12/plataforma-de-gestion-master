namespace api_gestiona.Entities
{
    public class BrechaUnidad
    {
        public long BrechaId { get; set; }
        public long DivisionId { get; set; }
        public Brecha Brecha { get; set; } = null!;
        public Division Division { get; set; } = null!;
    }
}
