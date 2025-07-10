using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Shared.Entities
{
    public class Comuna
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long? ProvinciaId { get; set; }
        public long? RegionId { get; set; }

    }
}