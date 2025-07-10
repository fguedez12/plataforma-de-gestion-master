using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class NotasCertificado 
    {
        public long Id { get; set; }
        public string UsuarioId { get; set; }
        public string Email { get; set; }
        public double NotaFinal { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Codigo { get; set; }
        public int Duracion { get; set; }
        public long CertificadoId { get; set; }
        public int Version { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Usuario Usuario { get; set; }
        public Certificado Certificado { get; set; }
    }
}
