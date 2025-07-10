namespace GobEfi.Web.Data.Entities
{
    public class ProcedimientoPapel : Documento
    {
        public bool DifusionInterna { get; set; }
        public bool Implementacion { get; set; }
        public bool ImpresionDobleCara { get; set; }
        public bool BajoConsumoTinta { get; set; }
        public Politica Politica { get; set; }
    }
}
