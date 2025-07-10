namespace api_gestiona.DTOs.Divisiones
{
    public class DivisionListDTO
    {
        public long Id { get; set; }
        public string Direccion { get; set; } = null!;
        public float? IndicadorEnegia { get; set; }
    }
}
