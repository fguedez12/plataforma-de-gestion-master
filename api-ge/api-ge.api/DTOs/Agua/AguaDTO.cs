using System.ComponentModel.DataAnnotations;

namespace api_gestiona.DTOs.Agua
{
    public class AguaDTO
    {
        public long Id { get; set; }
        public int TipoSuministroId { get; set; }
        public bool CompraAgregada { get; set; }
        public DateTime? InicioLectura { get; set; }
        public DateTime? FinLectura { get; set; }
        public DateTime? Fecha { get; set; }
        [Range(2022, 9999)]
        public int? AnioAdquisicion { get; set; }
        public double Cantidad { get; set; }
        public int? Costo { get; set; }
        public long DivisionId { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? AdjuntoNombre { get; set; }
    }
}
