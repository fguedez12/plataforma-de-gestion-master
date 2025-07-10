using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.SueloModels
{
    public class SueloModel
    {
        public long Id { get; set; }
        public long MaterialidadId { get; set; }
        public long AislacionId { get; set; }
        public double Superficie { get; set; }
        public long? EspesorId { get; set; }
    }
}
