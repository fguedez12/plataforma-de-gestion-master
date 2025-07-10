using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.PisoModels;
using GobEfi.Web.Models.RegionModels;
using GobEfi.Web.Models.UnidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.InmuebleModels
{
    public class InmuebleModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public string Direccion { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public long ComunaId { get; set; }
        public string Comuna { get; set; }
        public long RegionId { get; set; }
        public string Region { get; set; }
        public int TipoInmueble { get; set; }
        public float? Superficie { get; set; }
        public long? TipoUsoId { get; set; }
        public int? InstitucionResponsableId { get; set; }
        public int? ServicioResponsableId { get; set; }
        public string AdministracionServicioNombre { get; set; }
        public string AdministracionInstitucionNombre { get; set; }
        public string TipoAdministracionId { get; set; }
        public long? AdministracionServicioId { get; set; }
        public long? AdministracionMinisterioId { get; set; }
        public int AnyoConstruccion { get; set; }
        public string NroRol { get; set; }
        public List<InmuebleModel> Children { get; set; }
        public List<PisoModel> Pisos { get; set; }
        public List<UnidadInmuebleModel> UnidadesInmuebles { get; set; }


    }
}
