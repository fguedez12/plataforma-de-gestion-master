namespace api_gestiona.Entities
{
    public class Artefacto : BaseEntity
    {
        public long TipoArtefactoId { get; set; }
        public int CantidadArtefactos { get; set; }
        public int EstadoId { get; set; }
        public bool TecnologiaAhorro { get; set; }
        public int TecnologiaAhorroPorcentaje { get; set; }
        public long DivisionId { get; set; }
        public Division Division { get; set; }
        public TipoArtefacto TipoArtefacto { get; set; }
    }
}
