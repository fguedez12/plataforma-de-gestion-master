namespace api_gestiona.Entities
{
    public class ProcedimientoResiduo : Documento
    {
        public bool EntregaCertificadoRegistroRetiro { get; set; }
        public bool EntregaCertificadoRegistroDisposicion { get; set; }
        public bool EntregaCertificadoRegistroCantidades { get; set; }
        public Politica Politica { get; set; }
    }
}
