namespace api_gestiona.Entities
{
    public class Institucion
    {
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public bool Active { get; set; }
        public List<UsuarioInstitucion> UsuariosInstituciones { get; set; }

    }
}
