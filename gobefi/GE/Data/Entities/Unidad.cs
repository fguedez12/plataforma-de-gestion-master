using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Unidad : BaseEntity
    {
        public string Nombre { get; set; }
        public long ServicioId { get; set; }
        public bool ChkNombre { get; set; }
        public long? OldId { get; set; }
        public int Funcionarios { get; set; }
        public bool ReportaPMG { get; set; }
        public bool IndicadorEE { get; set; }
        public int AccesoFactura { get; set; }
        public int? InstitucionResponsableId { get; set; }
        public int? ServicioResponsableId { get; set; }
        public string OrganizacionResponsable { get; set; }
        public Servicio Servicio { get; set; }
        public List<UnidadInmueble> UnidadInmuebles { get; set; }
        public List<UnidadPiso> UnidadPisos { get; set; }
        public List<UnidadArea> UnidadAreas { get; set; }
        public List<UsuarioUnidad> UsuarioUnidades { get; set; }
    }
}
