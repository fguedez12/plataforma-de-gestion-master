namespace api_gestiona.Entities
{
    public class ListaIntegrante : Documento
    {
        public ActaComite ActaComite { get; set; }
        public List<Integrante> Integrantes { get; set; }
    }
}
