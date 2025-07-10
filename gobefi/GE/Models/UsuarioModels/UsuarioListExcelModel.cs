using System;

namespace GobEfi.Web.Models.UsuarioModels
{
    public class UsuarioListExcelModel
    {

        public string Institucion { get; set; }
        public string Servicio { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Rut { get; set; }


        public string Sexo { get; set; }
        public string Region { get; set; }


        public string NumeroTelefono { get; set; }
        public string Certificado { get; set; }
        public string Validado { get; set; }
        public string TipoGestor { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
