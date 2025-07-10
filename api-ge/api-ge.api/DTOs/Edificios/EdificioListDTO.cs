namespace api_gestiona.DTOs.Edificios
{
    public class EdificioListDTO
    {
        public long Id { get; set; }
        public string? Direccion { get; set; }
        public long ComunaId { get; set; }
        public string Comuna { get; set; }
        public long RegionId { get; set; }
        public string Region { get; set; }
    }
}
