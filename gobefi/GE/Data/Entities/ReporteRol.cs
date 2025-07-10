

namespace GobEfi.Web.Data.Entities
{
    public class ReporteRol
    {
        public long Id { get; set; }
        public long ReporteId { get; set; }
        public string RolId { get; set; }

        public Reporte Reporte { get; set; }
        public Rol Rol { get; set; }
    }
}