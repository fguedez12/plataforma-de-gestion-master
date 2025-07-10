using GobEfi.Web.Models.TipoArchivoModels;

namespace GobEfi.Web.Models.ReporteModels
{
    public class ReporteModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string RutaDondeObtenerArchivo { get; set; }
        public long TipoArchivoId { get; set; }
        public int Order { get; set; }
        public int Seccion { get; set; }

        public TipoArchivoModel TipoArchivo { get; set; }
    }
}