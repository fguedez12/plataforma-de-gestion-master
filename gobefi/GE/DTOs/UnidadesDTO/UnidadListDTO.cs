using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.DTOs.UnidadesDTO
{
    public class UnidadListDTO
    {
        public long Id { get; set; }
        public long? OldId { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public long InstitucionId { get; set; }
        public string InstitucionNombre { get; set; }
        public long ServicioId { get; set; }
        public string ServicioNombre { get; set; }
        public string Active { get; set; }
        public int Funcionarios { get; set; }
        public bool ReportaPMG { get; set; }
        public bool IndicadorEE { get; set; }
        public string AccesoFactura { get; set; }
        public int? InstitucionResponsableId { get; set; }
        public string InstitucionResponsableNombre { get; set; }
        public int? ServicioResponsableId { get; set; }
        public string ServicioResponsableNombre { get; set; }
        public string OrganizacionResponsable { get; set; }
    }
}
