using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.UnidadModels
{
    public class UnidadToSave
    {
        public long? OldId { get; set; }
        public string Nombre { get; set; }
        public long ServicioId { get; set; }
        public long InstitucionId { get; set; }
        public int Funcionarios { get; set; }
        public bool ReportaPMG { get; set; }
        public bool IndicadorEE { get; set; }
        public int AccesoFactura { get; set; }
        public int? InstitucionResponsableId { get; set; }
        public int? ServicioResponsableId { get; set; }
        public string OrganizacionResponsable { get; set; }
        public List<InmueblesToSave> Inmuebles { get; set; }
       
    }
}
