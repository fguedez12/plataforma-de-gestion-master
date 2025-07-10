namespace api_gestiona.Entities
{
    public class Politica : Documento   
    {
        public bool GestionPapel { get; set; }
        public bool EficienciaEnergetica { get; set; }
        public bool ComprasSustentables { get; set; }
        public string? Otras { get; set; }
        public List<DifusionPolitica> Difusiones { get; set; }
    }
}
