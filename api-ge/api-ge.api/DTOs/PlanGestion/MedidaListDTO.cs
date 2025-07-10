using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.PlanGestion
{
    public class MedidaListDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
