namespace api_gestiona.Entities
{
    public class Edificio
    {
        public  long Id { get; set; }
        public string? Direccion { get; set; }
        public bool Active { get; set; }
        public long ComunaId { get; set; }
        public Comuna Comuna { get; set; }
        public List<Division> Divisiones { get; set; }
    }
}
