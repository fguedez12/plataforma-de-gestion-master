namespace api_gestiona.Entities
{
    public class Comuna
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long RegionId { get; set; }
        public Region Region { get; set; }
    }
}
