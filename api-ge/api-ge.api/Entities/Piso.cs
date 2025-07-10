namespace api_gestiona.Entities
{
    public class Piso : BaseEntity
    {
        public double Superficie { get; set; }

        public double Altura { get; set; }

        public long NumeroPisoId { get; set; }
        public long? TipoUsoId { get; set; }
        public long? EdificioId { get; set; }
        public long? DivisionId { get; set; }
        public List<Area>? Areas { get; set; }
    }
}
