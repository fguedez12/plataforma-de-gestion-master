using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CertificadoModels
{
    public class NotaModel
    {
        public string UsuarioId { get; set; }
        public string Email { get; set; }
        public double NotaFinal { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Codigo { get; set; }
        public int Duracion { get; set; }
        public long CertificadoId { get; set; }
        public string NombreCertificado { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TipoGestor { get; set; }
        public string Ministerio { get; set; }
        public string Servicio { get; set; }
        public long ServicioId { get; set; }
    }
}
