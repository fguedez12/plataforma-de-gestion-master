using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.FV.Shared.Entities
{
    public class ModeloOtro : IId
    {
        public long Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Traccion { get; set; }
        public string Transmision { get; set; }
        public int PropulsionId { get; set; }
        public int CombustibleId { get; set; }
        public string Cilindrada { get; set; }
        public string Carroceria { get; set; }
        public Propulsion Propulsion { get; set; }
        public Combustible Combustible { get; set; }

    }
}
