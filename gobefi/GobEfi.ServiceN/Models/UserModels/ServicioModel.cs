namespace GobEfi.ServiceN.Models.UserModels
{
    public class ServicioModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public string Nombre { get; set; }
    }
}
