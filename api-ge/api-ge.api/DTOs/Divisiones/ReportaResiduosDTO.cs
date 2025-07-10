namespace api_gestiona.DTOs.Divisiones
{
    public class ReportaResiduosDTO
    {
        public bool CheckReporta { get; set; }
        public string? Justificacion { get; set; }
        public bool CheckReportaNoReciclados { get; set; }
        public string? JustificacionNoReciclados { get; set; }
    }
}
