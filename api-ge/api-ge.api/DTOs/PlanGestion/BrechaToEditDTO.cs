using api_gestiona.DTOs.Divisiones;

namespace api_gestiona.DTOs.PlanGestion
{
    public class BrechaToEditDTO
    {
        public long Id { get; set; }
        public long DimensionId { get; set; }
        public long TipoBrecha { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public List<DivisionListDTO>? Unidades { get; set; }
    }
}
