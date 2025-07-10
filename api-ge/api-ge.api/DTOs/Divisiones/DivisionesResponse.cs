using api_gestiona.DTOs.Instituciones;
using api_gestiona.DTOs.Servicios;

namespace api_gestiona.DTOs.Divisiones
{
    public class DivisionesResponse
    {
        public bool Ok { get; set; }
        public string? Msj { get; set; }
        public InstitucionListDTO Institucion { get; set; }
        public ServicioDTO Servicio { get; set; }
        public DivisionDTO Division { get; set; }
        public List<DivisionDTO> Divisiones { get; set; }
    }
}
