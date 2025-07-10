namespace api_gestiona.Entities
{
    public class TipoLuminaria
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public float Q_Educacion { get; set; }
        public float Q_Oficinas { get; set; }
        public float Q_Salud { get; set; }
        public float Q_Seguridad { get; set; }
        public float Area_Educacion { get; set; }
        public float Area_Oficinas { get; set; }
        public float Area_Salud { get; set; }
        public float Area_Seguridad { get; set; }
        public int Vida_Util { get; set; }
        public int Costo_Lamp { get; set; }
        public int Costo_Lum { get; set; }
        public int Costo_Social_Lamp { get; set; }
        public int Costo_Social_Lum { get; set; }
        public float Ejec_HD_Maestro { get; set; }
        public float Ejec_HD_Ayte { get; set; }
        public float Ejec_HD_Jornal { get; set; }
        public float Rep_HD_Maestro { get; set; }
        public float Rep_HD_Ayte { get; set; }
        public float Rep_HD_Jornal { get; set; }
    }
}
