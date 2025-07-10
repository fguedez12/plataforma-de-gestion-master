namespace api_gestiona.Entities
{
    public class TipoEquipoCalefaccionEnergetico
    {
        public long Id { get; set; }
        public long TipoEquipoCalefaccionId { get; set; }
        public long EnergeticoId { get; set; }
        public TipoEquipoCalefaccion TipoEquipoCalefaccion { get; set; } = null!;
        public Energetico Energetico { get; set; } = null!;
    }
}
