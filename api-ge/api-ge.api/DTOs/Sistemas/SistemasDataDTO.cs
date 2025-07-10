namespace api_gestiona.DTOs.Sistemas
{
    public class SistemasDataDTO
    {
        public long? TipoLuminariaId { get; set; }
        public long? EquipoCalefaccionId { get; set; }
        public long? EnergeticoCalefaccionId { get; set; }
        public long? TempSeteoCalefaccionId { get; set; }
        public long? EquipoRefrigeracionId { get; set; }
        public long? EnergeticoRefrigeracionId { get; set; }
        public long? TempSeteoRefrigeracionId { get; set; }
        public long? EquipoAcsId { get; set; }
        public long? EnergeticoAcsId { get; set; }
        public bool SistemaSolarTermico { get; set; }
        public long? ColectorId { get; set; }
        public float? SupColectores { get; set; }
        public bool FotoTecho { get; set; }
        public float? SupFotoTecho { get; set; }
        public bool InstTerSisFv { get; set; }
        public float? SupInstTerSisFv { get; set; }
        public bool ImpSisFv { get; set; }
        public float? SupImptSisFv { get; set; }
        public float? PotIns { get; set; }
        public int? MantColectores { get; set; }
        public int? MantSfv { get; set; }
        public List<LuminariasListDTO>? Luminarias { get; set; }
        public List<EquiposCalefaccionListDTO>? EquiposCalefaccion { get; set; }
        public List<EquiposCalefaccionListDTO>? EquiposRefrigeracion { get; set; }
        public List<EquiposCalefaccionListDTO>? EquiposAc { get; set; }
    }
}
