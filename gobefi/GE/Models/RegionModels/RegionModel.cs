using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.RegionModels
{
    public class RegionModel : BaseModel<long>
    {
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public int Posicion { get; set; }
        public ICollection<Provincia> Provincias { get; set; }
        public ICollection<Comuna> Comunas { get; set; }
    }
}
