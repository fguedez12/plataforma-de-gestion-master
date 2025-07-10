namespace api_gestiona.DTOs.Pagination
{
    public class PaginationDTO
    {
        public int Page { get; set; }
        private int recordsPerPage = 5;
        private readonly int maxRecordsPerPage = 50;
        public int RecordsPerPage
        {
            get
            {
                return recordsPerPage;
            }
            set
            {
                recordsPerPage = (value > maxRecordsPerPage) ? maxRecordsPerPage : value;
            }
        }
        public string? SearchText { get; set; }
        public long? ServicioId { get; set; }
        public long? InstitucionId { get; set; }
        public long? DivisionId { get; set; }
        public bool Pmg { get; set; }
        public long? AnioDoc { get; set; }
        public int Etapa { get; set; }
    }
}
