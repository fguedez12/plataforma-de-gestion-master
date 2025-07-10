using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class Pais
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public List<Aeropuerto> Aeropuertos { get; set; }
    }
}
