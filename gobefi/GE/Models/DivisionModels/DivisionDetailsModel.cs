using System.ComponentModel;

namespace GobEfi.Web.Models.DivisionModels
{
    public class DivisionDetailsModel
    {
        public long Id { get; set; }
        public long ComunaId { get; set; }
        [DisplayName("Edificio")]
        public long EdificioId { get; set; }
        public long InstitucionId { get; set; }
        [DisplayName("Servicio")]
        public long ServicioId { get; set; }
        [DisplayName("Nombre Unidad")]
        public string NombreUnidad { get; set; }
        [DisplayName("Tipo de Uso")]
        public long? TipoDeUsoId { get; set; }
        public string Pisos { get; set; }

        [DisplayName("Superficie (m²)")]
        public string Superficie { get; set; }
        [DisplayName("Tipo de Propiedad")]
        public long TipoPropiedadId { get; set; }

        [DisplayName("N° de Rol")]
        public string NroRol { get; set; }
        [DisplayName("No posee Rol")]
        public bool SinRol { get; set; }
        [DisplayName("Justificación")]
        public string JustificaRol { get; set; }

        [DisplayName("Año de Construcción")]
        public int AnioConstruccion { get; set; }

        [DisplayName("N° de Funcionarios")]
        public int NroFuncionarios { get; set; }


        public bool ComparteMedidorElectricidad { get; set; }
        public bool ComparteMedidorGasCanieria { get; set; }
    }
}
