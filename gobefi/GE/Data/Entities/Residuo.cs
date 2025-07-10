using System;

namespace GobEfi.Web.Data.Entities
{
    public class Residuo : BaseEntity
    {
        public bool IngresoAgregado { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Anio { get; set; }
        public string TipoResiduo { get; set; }
        public int Cantidad { get; set; }
        public long DivisionId { get; set; }
        public long? ProcedimientoId { get; set; }
        public Division Division { get; set; }
    }
}
