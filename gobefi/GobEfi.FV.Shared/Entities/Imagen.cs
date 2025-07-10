using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.FV.Shared.Entities
{
    public class Imagen
    {
        public long Id { get; set; }
        public string Url { get; set; }
        public long VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
