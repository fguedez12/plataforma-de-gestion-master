using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.UsuarioModels
{
    public class UsuarioIndexModel : BasePagedModel<UsuarioListModel>
    {
        bool _activo = true;

        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Rut { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }


        [DisplayName("Instituciones")]
        public long InstitucionId { get; set; }
        [DisplayName("Servicios")]
        public long ServicioId { get; set; }


        public bool? Validado { get; set; }
        public bool? Certificado { get; set; }
        public bool Activo
        {
            get { return this._activo; }

            set { this._activo = value; }
        }
    }
}
