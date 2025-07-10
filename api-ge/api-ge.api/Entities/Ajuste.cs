namespace api_gestiona.Entities
{
    public class Ajuste : BaseEntity
    {
        public bool EditUnidadPMG { get; set; }
        public bool DeleteUnidadPMG { get; set; }
        public bool ComprasServicio { get; set; }
        public bool CreateUnidadPMG { get; set; }
        public bool ActiveAlcanceModule { get; set; }
    }
}
