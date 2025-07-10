namespace api_gestiona.Entities
{
    public class CompraMedidor
    {
        public long Id { get; set; }
        public double Consumo { get; set; }
        public long MedidorId { get; set; }
        public long CompraId { get; set; }
        public long? ParametroMedicionId { get; set; }
        public long? UnidadMedidaId { get; set; }
    }
}
