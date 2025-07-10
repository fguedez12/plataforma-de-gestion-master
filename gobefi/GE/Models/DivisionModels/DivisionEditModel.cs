using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DivisionModels
{
    public class DivisionEditModel
    {
        public long Id { get; set; }

        [DisplayName("Region")]
        public long? RegionId { get; set; }
        [DisplayName("Provincia")]
        public long? ProvinciaId { get; set; }
        [DisplayName("Comuna")]
        public long? ComunaId { get; set; }

        [DisplayName("Edificio")]
        public long EdificioId { get; set; }
        [DisplayName("Servicio")]
        public long ServicioId { get; set; }
        [DisplayName("Institución")]
        public long InstitucionId { get; set; }


        [DisplayName("Nombre Unidad")]
        public string NombreUnidad { get; set; }
        [DisplayName("Tipo de Uso")]
        public long? TipoDeUsoId { get; set; }
        public string Pisos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Valor debe ser mayor a 0")]
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

        [Range(1, int.MaxValue, ErrorMessage = "Valor debe ser mayor a 0")]
        [DisplayName("N° colaboradores del servicio")]
        public int NroFuncionarios { get; set; }
        [DisplayName("N° Otros colaboradores")]
        public int NroOtrosColaboradores { get; set; }

        [DisplayName("Se incluye  para reporte consumo energía del PMG")]
        public bool ReportaPMG { get; set; }
        [DisplayName("Se incluye para Indicador EE del PMG")]
        public bool IndicadorEE { get; set; }
        [DisplayName("Se incluye para reporte de gestión ambiental del PMG")]
        public bool ReportaEV { get; set; }
        [DisplayName("Activa")]

        public bool Active { get; set; }

        public bool ComparteMedidorElectricidad { get; set; }
        public bool ComparteMedidorGasCanieria { get; set; }
        public bool? DisponeVehiculo { get; set; }
        public List<string> VehiculosIds { get; set; }
        public bool DisponeCalefaccion { get; set; }
        public bool AireAcondicionadoElectricidad { get; set; }
        public bool CalefaccionGas { get; set; }

        [DisplayName("Tiene acceso a las facturas de tus consumos")]
        public int? AccesoFactura { get; set; }
        [DisplayName("Institución responsable de los consumos")]
        public int? InstitucionResponsableId { get; set; }
        [DisplayName("Servicio responsable de los consumos")]
        public int? ServicioResponsableId { get; set; }
        [DisplayName("Organización responsable de los consumos")]
        public string OrganizacionResponsable { get; set; }
        public bool TieneMedidorElectricidad { get; set; }
        public bool TieneMedidorGas { get; set; }
        [DisplayName("Tiene acceso a las facturas de Agua")]
        public int? AccesoFacturaAgua { get; set; }
        [DisplayName("Institución responsable de los consumos")]
        public int? InstitucionResponsableAguaId { get; set; }
        [DisplayName("Servicio responsable de los consumos")]
        public int? ServicioResponsableAguaId { get; set; }
        [DisplayName("Organización responsable de los consumos")]
        public string OrganizacionResponsableAgua { get; set; }
        public bool ComparteMedidorAgua { get; set; }
        public bool? GestionBienes { get; set; }
        public bool CargaPosteriorT { get; set; }
        public DateTime CreatedAt { get; set; }

        [DisplayName("Observaciones de inexistencia para energía y vehículos")]
        public string ObsInexistenciaEyV { get; set; }
    }
}
