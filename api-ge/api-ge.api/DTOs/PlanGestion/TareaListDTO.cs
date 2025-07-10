using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.PlanGestion
{
    public class TareaListDTO
    {
        public long Id { get; set; }
        public long DimensionBrechaId { get; set; }
        public long AccionId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Responsable { get; set; }
        public string EstadoAvance { get; set; }
        public string Observaciones { get; set; }
        
        // Nuevo campo agregado según Tarea #2
        public string DescripcionTareaEjecutada { get; set; }
        
        public string DimensionBrechaDescripcion { get; set; }
        public string AccionDescripcion { get; set; }
    }
}