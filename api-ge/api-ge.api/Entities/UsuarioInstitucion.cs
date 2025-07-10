namespace api_gestiona.Entities
{
    public class UsuarioInstitucion
    {
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public long InstitucionId { get; set; }
        public Institucion Institucion { get; set; }
    }
}
