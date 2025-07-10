using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.ServicesV2_1.Models
{
    public class JerarquiaModel
    {
        public List<Institucion> Instituciones { get; set; }

        public List<Servicio> Servicios { get; set; }

        public List<Edificio> Edificios { get; set; }

        public List<Division> Divisiones { get; set; }

        public List<Region> Regiones { get; set; }

        public List<Provincia> Provincias { get; set; }

        public List<Comuna> Comunas { get; set; }
    }
}
