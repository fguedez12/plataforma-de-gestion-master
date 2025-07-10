namespace api_gestiona.DTOs.PlanGestion
{
    public class FiltrosDTO
    {
        public List<DimensionFiltroDTO> Dimensiones { get; set; } = new List<DimensionFiltroDTO>();
        public List<ObjetivoFiltroDTO> Objetivos { get; set; } = new List<ObjetivoFiltroDTO>();
        public List<int> Anios { get; set; } = new List<int>();
    }

    public class DimensionFiltroDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreNormalizado { get; set; } = null!;
    }

    public class ObjetivoFiltroDTO
    {
        public long Id { get; set; }
        public long DimensionBrechaId { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
}