using GobEfi.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services.Request
{
    public class UsuarioRequest : BaseRequest
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rut { get; set; }
        public string Email { get; set; }
        public long InstitucionId { get; set; }
        public long ServicioId { get; set; }
        public bool? Validado { get; set; }
        public bool? Certificado { get; set; }
        public bool Activo { get; set; } = true;
    }
}
