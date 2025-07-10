namespace api_gestiona.DTOs
{
    public class CharlasDTO
    {
        public long Id { get; set; }
        public long TipoDocumentoId { get; set; }
        public string? TipoDocumentoNombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string? AdjuntoUrl { get; set; }
        public string? Adjunto { get; set; }
        public string? AdjuntoPath { get; set; }
        public string? AdjuntoNombre { get; set; }
        public string? AdjuntoUrlInvitacion { get; set; }
        public string? AdjuntoNombreinvitacion { get; set; }
        public string Titulo { get; set; }
        public int NParticipantes { get; set; }
        public long ServicioId { get; set; }
        public string? Materia { get; set; }
        public string? Observaciones { get; set; }
        public int EtapaSEV_docs { get; set; }

        public bool? GestionEnergia { get; set; }
        public bool? GestionVehiculosTs { get; set; }
        public bool? TrasladoSustentable { get; set; }
        public bool? GestionAgua { get; set; }
        public bool? MateriaGestionPapel { get; set; }
        public bool? GestionResiduosEc { get; set; }
        public bool? GestionComprasS { get; set; }
        public bool? GestionBajaBs { get; set; }
        public bool? HuellaC { get; set; }
        public bool? CambioClimatico { get; set; }
        public bool? OtraMateria { get; set; }

    }
}
