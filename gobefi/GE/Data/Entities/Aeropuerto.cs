namespace GobEfi.Web.Data.Entities
{
    public class Aeropuerto
    {
        public long Id { get; set; }
        public string Iata { get; set; }
        public string Nombre { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }
        public long PaisId { get; set; }
        public Pais Pais { get; set; }

    }
}
