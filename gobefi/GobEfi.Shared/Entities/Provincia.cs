using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Shared.Entities
{
    public class Provincia
    {
        private long _regionId;
        private string _nombre;


        public long Id { get; set; }
        public long RegionId { get => _regionId; set => _regionId = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }

    }
}