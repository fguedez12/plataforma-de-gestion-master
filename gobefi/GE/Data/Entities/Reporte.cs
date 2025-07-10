using System.Collections;
using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class Reporte
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string RutaDondeObtenerArchivo { get; set; }
        public long TipoArchivoId { get; set; }
        public bool EsPMG { get; set; }
        public bool SeGeneraAutomatico { get; set; }
        public string ProcedimientoAlmacenado { get; set; }
        public bool Activo { get; set; }
        public int Order { get; set; }
        public int Seccion { get; set; }
        public ICollection<ReporteRol> ReportesRol { get; set; }
        public TipoArchivo TipoArchivo { get; set; }
    }
}