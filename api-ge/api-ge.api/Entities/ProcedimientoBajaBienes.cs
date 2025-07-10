namespace api_gestiona.Entities
{
    public class ProcedimientoBajaBienes : Documento
    {
        public bool BajaBienesFormalizado { get; set; }
        public Politica Politica { get; set; }
    }
}
