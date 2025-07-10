namespace api_gestiona.Entities
{
    public class Unidad : BaseEntity
    {
        public string Nombre { get; set; }
        public long ServicioId { get; set; }
        public bool ChkNombre { get; set; }
        public long? OldId { get; set; }
    }
}
