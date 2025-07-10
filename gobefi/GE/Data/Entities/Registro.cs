using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Registro
    {
        public long Id { get; set; }
        public string Email { get; set; }

        public string Nombres { get; set; }
       
        public string Apellidos { get; set; }
       
        public string Telefono { get; set; }
       
        public long MinisterioId { get; set; }
        
        public long ServicioId { get; set; }
        public string OtroServicio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
