using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.FV.Shared.Entities
{
    public class Combustible : IId
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }
}
