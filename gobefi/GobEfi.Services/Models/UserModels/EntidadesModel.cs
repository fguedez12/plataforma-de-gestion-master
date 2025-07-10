using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Models.UserModels
{
    public class EntidadesModel
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rut { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
        public List<RolesModel> Roles { get; set; }
        public List<InstitucionModel> Instituciones { get; set;  }
        public List<ServicioModel> Servicios { get; set; }
        public List<DivisionModel> Divisiones { get; set; }
    }
}
