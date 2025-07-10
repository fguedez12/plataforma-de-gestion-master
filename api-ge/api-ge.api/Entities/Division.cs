using System.ComponentModel;

namespace api_gestiona.Entities
{
    public class Division : BaseEntity
    {
        public List<Piso> ListPisos { get; set; }
        public string? Direccion { get; set; }
        public string? Pisos { get; set; }
        public long ServicioId { get; set; }
        public int GeVersion { get; set; }
        public int? AnioInicioGestionEnergetica { get; set; }
        public int? AnioInicioRestoItems { get; set; }
        public int Funcionarios { get; set; }
        public int NroOtrosColaboradores { get; set; }
        public bool ReportaPMG { get; set; }
        public bool IndicadorEE { get; set; }
        public bool ReportaEV { get; set; }
        public bool ObservaPapel { get; set; }
        public string? ObservacionPapel { get; set; }
        public bool ObservaResiduos { get; set; } = true;
        public string? ObservacionResiduos { get; set; }
        public bool JustificaResiduos { get; set; }
        public string? JustificacionResiduos { get; set; }

        public bool JustificaResiduosNoReciclados { get; set; }
        public string? JustificacionResiduosNoReciclados { get; set; }
        public bool ObservaAgua { get; set; } = true;
        public string? ObservacionAgua { get; set; }
        public bool? TieneMedidorElectricidad { get; set; }
        public bool? TieneMedidorGas { get; set; }
        public int? AccesoFacturaAgua { get; set; }
        public int? AccesoFactura { get; set; }
        public int? InstitucionResponsableAguaId { get; set; }
        public int? ServicioResponsableAguaId { get; set; }
        public string? OrganizacionResponsableAgua { get; set; }
        public bool? ComparteMedidorAgua { get; set; }
        public bool ComparteMedidorElectricidad { get; set; }
        public bool ComparteMedidorGasCanieria { get; set; }
        public string? VehiculosIds { get; set; }
        public bool? NoDeclaraImpresora { get; set; }
        public bool? NoDeclaraArtefactos { get; set; }
        public bool? NoDeclaraContenedores { get; set; }
        public bool? GestionBienes { get; set; }
        public long EdificioId { get; set; }
        public string? NroRol { get; set; }
        public bool SinRol { get; set; }
        public string? JustificaRol { get; set; }
        public bool? DisponeVehiculo { get; set; }
        public bool DisponeCalefaccion { get; set; }
        public bool AireAcondicionadoElectricidad { get; set; }
        public bool CalefaccionGas { get; set; }
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
        public float? IndicadorEnegia { get; set; }
        public string? ObsInexistenciaEyV { get; set; }
        public Servicio Servicio { get; set; }
        public List<Resma> Resmas { get; set; }
        public List<Agua> Aguas { get; set; }
        public List<Artefacto> Artefactos { get; set; }
        public List<Residuo> Residuos { get; set; }
        public List<Contenedor> Contenedores { get; set; }
        public Edificio Edificio { get; set; }
        public List<BrechaUnidad>? BrechaUnidades { get; set; }
        public ICollection<AccionUnidad>? AccionUnidades { get; set; }
    }
}
