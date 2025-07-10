namespace api_gestiona.DTOs.Divisiones
{
    public class DivisionDTO
    {
        public long Id { get; set; }
        public string Direccion { get; set; }
        public string Pisos { get; set; }
        public string? AnioInicioGestionEnergetica { get; set; }
        public string? AnioInicioRestoItems { get; set; }
    }
}
