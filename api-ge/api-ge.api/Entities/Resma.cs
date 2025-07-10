namespace api_gestiona.Entities
{
    public class Resma : BaseEntity
    {
        public bool Agregada { get; set; }
        public int? CantidadResmasRecicladas { get; set; }
        public int? PapelRecicladoRango { get; set; }
        public int CantidadResmas { get; set; }
        public DateTime? FechaAdquisicion { get; set; }
        public int? AnioAdquisicion { get; set; }
        public int CostoEstimado { get; set; }
        public string? IdMercadoPublico { get; set; }
        public long DivisionId { get; set; }
        public Division Division { get; set; }
    }
}
