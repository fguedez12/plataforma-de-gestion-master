namespace api_gestiona.Entities
{
    public class Area : BaseEntity
    {
        public string? Nombre { get; set; }
        public decimal Superficie { get; set; }
        public long TipoUsoId { get; set; }
        public long PisoId { get; set; }
    }
}
