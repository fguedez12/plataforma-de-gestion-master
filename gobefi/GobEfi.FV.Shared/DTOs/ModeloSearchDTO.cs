using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.FV.Shared.DTOs
{
    public class ModeloSearchDTO
    {
        public long Id { get; set; }
        public long IdEm { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
    }
}
