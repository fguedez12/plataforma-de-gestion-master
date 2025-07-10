namespace api_gestiona.DTOs.MedicionInteligente.ResponseDTO
{
    public class DataSetDTO
    {
        public string Label { get; set; }
        public List<double> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
        public List<string> BorderColor { get; set; }
        public string BorderWidth { get; set; }

    }
}
