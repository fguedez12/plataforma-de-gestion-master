using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Division : BaseEntity
    {
        public int Funcionarios { get; set; }
        public int NroOtrosColaboradores { get; set; }
        public string Nombre { get; set; }
        public string Pisos { get; set; }
        public bool ReportaPMG { get; set; }
        public bool IndicadorEE { get; set; }
        public bool ReportaEV { get; set; }
        public int AnyoConstruccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public double? Superficie { get; set; }
        public long EdificioId { get; set; }
        public long ServicioId { get; set; }
        public long? TipoUnidadId { get; set; }
        public long TipoPropiedadId { get; set; }
        public long? TipoUsoId { get; set; }
        [MaxLength(255)]
        public string NroRol { get; set; }
        public bool SinRol { get; set; }
        public string JustificaRol { get; set; }
        public string Direccion { get; set; }
        public bool ComparteMedidorElectricidad { get; set; }
        public bool ComparteMedidorGasCanieria { get; set; }
        public bool PisosIguales { get; set; }
        public int NivelPaso3 { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public long? RegionId { get; set; }
        public long? ProvinciaId { get; set; }
        public long? ComunaId { get; set; }
        public long? TipoInmueble { get; set; }
        public long? TipoAdministracionId { get; set; }
        public long? AdministracionServicioId { get; set; }
        public long? ParentId { get; set; }
        public int GeVersion { get; set; }
        public bool DpSt1 { get; set; }
        public bool DpSt2 { get; set; }
        public bool DpSt3 { get; set; }
        public bool DpSt4 { get; set; }
        public int AccesoFactura { get; set; }
        public int? InstitucionResponsableId { get; set; }
        public int? ServicioResponsableId { get; set; }
        public string OrganizacionResponsable { get; set; }
        public long? DireccionInmuebleId { get; set; }
        public bool? Compromiso2022 { get; set; }
        public int? EstadoCompromiso2022 { get; set; }
        public string Justificacion { get; set; }
        public string ObservacionCompromiso2022 { get; set; }
        public int? AnioInicioGestionEnergetica { get; set; }
        public int? AnioInicioRestoItems { get; set; }
        public bool? DisponeVehiculo { get; set; } = false;
        public string VehiculosIds { get; set; }
        public bool DisponeCalefaccion { get; set; }
        public bool AireAcondicionadoElectricidad { get; set; }
        public bool CalefaccionGas { get; set; }
        public bool ObservaPapel { get; set; } = true;
        public string ObservacionPapel { get; set; }
        public bool ObservaResiduos { get; set; } = true;
        public string ObservacionResiduos { get; set; }
        public bool JustificaResiduos { get; set; }
        public string JustificacionResiduos { get; set; }
        public bool JustificaResiduosNoReciclados { get; set; }
        public string JustificacionResiduosNoReciclados { get; set; }
        public bool ObservaAgua { get; set; } = true;
        public string ObservacionAgua { get; set; }
        public bool TieneMedidorElectricidad { get; set; }
        public bool TieneMedidorGas { get; set; }
        public int? AccesoFacturaAgua { get; set; }
        public int? InstitucionResponsableAguaId { get; set; }
        public int? ServicioResponsableAguaId { get; set; }
        public string OrganizacionResponsableAgua { get; set; }
        public bool ComparteMedidorAgua { get; set; }
        public bool? NoDeclaraImpresora { get; set; }
        public bool? NoDeclaraArtefactos { get; set; }
        public bool? NoDeclaraContenedores { get; set; }
        public bool? GestionBienes { get; set; }
        public bool UsaBidon { get; set; }
        public long? TipoLuminariaId { get; set; }
        public long? EquipoCalefaccionId { get; set; }
        public long? EnergeticoCalefaccionId { get; set; }
        public long? TempSeteoCalefaccionId { get; set; }
        public long? EquipoRefrigeracionId { get; set; }
        public long? EnergeticoRefrigeracionId { get; set; }
        public long? TempSeteoRefrigeracionId { get; set; }
        public long? EquipoAcsId { get; set; }
        public long? EnergeticoAcsId { get; set; }
        public bool SistemaSolarTermico { get; set; }
        public long? ColectorId { get; set; }
        public float? SupColectores { get; set; }
        public bool FotoTecho { get; set; }
        public float? SupFotoTecho { get; set; }
        public bool InstTerSisFv { get; set; }
        public float? SupInstTerSisFv { get; set; }
        public bool ImpSisFv { get; set; }
        public float? SupImptSisFv { get; set; }
        public float? PotIns { get; set; }
        public int? MantColectores { get; set; }
        public int? MantSfv { get; set; }
        public bool CargaPosteriorT { get; set; }
        public float? IndicadorEnegia { get; set; }
        public string ObsInexistenciaEyV { get; set; }
        public virtual Division Parent { get; set; }
        public virtual Comuna Comuna { get; set; }
        public virtual Edificio Edificio { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual TipoUnidad TipoUnidad { get; set; }
        public virtual TipoPropiedad TipoPropiedad { get; set; }
        public virtual TipoUso TipoUso { get; set; }
        public ICollection<Equipo> Equipos { get; set; }
        public ICollection<ArchivoAdjunto> Adjuntos { get; set; }
        public ICollection<SubDivision> SubDivisiones { get; set; }
        public ICollection<NumeroCliente> NumerosClientes { get; set; }
        public ICollection<Medidor> Medidores { get; set; }
        public ICollection<Compra> Compras { get; set; }
        public ICollection<EnergeticoDivision> EnergeticosDivision { get; set; }
        public ICollection<MedidorDivision> MedidorDivision { get; set; }
        public virtual ICollection<UsuarioDivision> UsuariosDivisiones { get; set; }
        public virtual ICollection<Trazabilidad> Trazabilidades { get; set; }
        public ICollection<MedidorInteligenteDivision> MedidorInteligenteDivisiones { get; set; }
        public virtual ICollection<ArchivoDp> ArchivosDp { get; set; }
        public virtual Region Region { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual Servicio AdministracionServicio { get; set; }
        public virtual ICollection<Piso> ListPisos { get; set; }
        public List<UnidadInmueble> UnidadInmuebles { get; set; }
        public Direccion DireccionInmueble { get; set; }
        public List<Resma> Resmas { get; set; }
        public List<Agua> Aguas { get; set; }
        public List<Artefacto> Artefactos { get; set; }
        public List<Residuo> Residuos { get; set; }
        public List<Contenedor> Contenedores { get; set; }
        public List<BrechaUnidad> BrechaUnidades { get; set; }
        public ICollection<AccionUnidad> AccionUnidades { get; set; }
    }
}
