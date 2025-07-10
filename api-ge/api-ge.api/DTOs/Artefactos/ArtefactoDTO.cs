namespace api_gestiona.DTOs.Artefactos
{
    public class ArtefactoDTO
    {
        public long Id { get; set; }
        public int TipoArtefactoId { get; set; }
        public int CantidadArtefactos { get; set; }
        public int EstadoId { get; set; }
        public bool TecnologiaAhorro { get; set; }
        public int? TecnologiaAhorroPorcentaje { get; set; }
        public long DivisionId { get; set; }
        public string? TipoArtefacto { get; set; }
    }
}
