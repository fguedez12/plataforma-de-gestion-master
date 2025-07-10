using api_gestiona.Entities;

namespace api_gestiona.DTOs.Residuos
{
    public class ResiduoDTO
    {
        public long Id { get; set; }
        public bool IngresoAgregado { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Anio { get; set; }
        public string TipoResiduo { get; set; }
        public int Cantidad { get; set; }
        public long DivisionId { get; set; }
        public long? ProcedimientoId { get; set; }
    }
}
