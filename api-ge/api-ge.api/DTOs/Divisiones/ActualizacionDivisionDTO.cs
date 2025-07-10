namespace api_gestiona.DTOs.Divisiones
{
    public class ActualizacionDivisionDTO
    {
        public string? NroRol { get; set; }
        public bool SinRol { get; set; }
        public string? JustificaRol { get; set; }
        public int Funcionarios { get; set; }
        public int NroOtrosColaboradores { get; set; }
        public bool? DisponeVehiculo { get; set; }
        public List<string> VehiculosIds { get; set; }
        public List<VehiculoDTO> Vehiculos { get; set; }
        public int? AccesoFacturaAgua { get; set; }
        public int? InstitucionResponsableAguaId { get; set; }
        public int? ServicioResponsableAguaId { get; set; }
        public string? OrganizacionResponsableAgua { get; set; }
        public bool? ComparteMedidorAgua { get; set; }
        public bool DisponeCalefaccion { get; set; }
        public bool AireAcondicionadoElectricidad { get; set; }
        public bool CalefaccionGas { get; set; }
        public bool? GestionBienes { get; set; }
    }
}
