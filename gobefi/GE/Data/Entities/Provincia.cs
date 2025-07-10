using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Provincia
    {
        private long _regionId;
        private string _nombre;
        private Region _region;
        private ICollection<Comuna> _comunas;

        public long Id { get; set; }
        public long RegionId { get => _regionId; set => _regionId = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public virtual Region Region { get => _region; set => _region = value; }
        public ICollection<Comuna> Comunas { get => _comunas; set => _comunas = value; }
        public virtual ICollection<Division> Inmuebles { get; set; }
        public List<Direccion> Direcciones { get; set; }
        public Provincia()
        {
            this.Comunas = new HashSet<Comuna>();
        }
    }
}