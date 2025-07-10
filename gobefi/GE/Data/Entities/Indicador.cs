using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class Indicador : BaseEntity
    {
        public long DimensionBrechaId { get; set; }
        public long ObjetivoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string Numerador { get; set; }
        public string Denominador { get; set; }
        public string UnidadMedida { get; set; }
        public double? Valor { get; set; }
        public string RespladoMonitoreo { get; set; }
        public DimensionBrecha DimensionBrecha { get; set; }
        public Objetivo Objetivo { get; set; }
    }
}
