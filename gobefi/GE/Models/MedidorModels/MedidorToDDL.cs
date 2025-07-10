namespace GE.Models.MedidorModels
{
    public class MedidorToDDL
    {
        public long Id { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public long NumeroClienteId { get; set; }
        public long? DivisionId { get; set; }
    }
}