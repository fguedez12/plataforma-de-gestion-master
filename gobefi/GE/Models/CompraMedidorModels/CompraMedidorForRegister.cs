namespace GobEfi.Web.Models.CompraMedidorModels
{
    public class CompraMedidorForRegister
    {
        public double Consumo { get; set; }
        public long MedidorId { get; set; }
        public long CompraId { get; set; }
        public long? ParametroMedicionId { get; set; }
        public long? UnidadMedidaId { get; set; }
    }
}