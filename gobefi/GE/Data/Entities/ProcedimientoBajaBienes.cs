namespace GobEfi.Web.Data.Entities
{
    public class ProcedimientoBajaBienes : Documento
    {
        public bool BajaBienesFormalizado { get; set; }
        public Politica Politica { get; set; }
    }
}
