using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Models.MedidoresModels
{
    public class UnidadModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public List<MedidoresModel> Medidores { get; set; } 
    }
}
