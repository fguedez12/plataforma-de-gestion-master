namespace GobEfi.Web.Data.Entities
{
    public class TipoEquipoCalefaccionEnergetico
    {
        public long Id { get; set; }
        public long TipoEquipoCalefaccionId { get; set; }
        public long EnergeticoId { get; set; }
        public TipoEquipoCalefaccion TipoEquipoCalefaccion { get; set; }
        public Energetico Energetico { get; set; }
    }
}
