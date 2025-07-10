using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Models.Entities
{
    public class Imagen
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public long VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
