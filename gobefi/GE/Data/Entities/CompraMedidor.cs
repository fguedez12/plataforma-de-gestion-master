namespace GobEfi.Web.Data.Entities
{
    public class CompraMedidor
    {
        public long Id { get; set; }
        public double Consumo { get; set; }
        public long MedidorId { get; set; }
        public long CompraId { get; set; }
        public long? ParametroMedicionId { get; set; }
        public long? UnidadMedidaId { get; set; }
        
        public virtual Medidor Medidor { get; set; }
        public virtual Compra Compra { get; set; }
        public virtual ParametroMedicion ParametroMedicion { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }

    }
}