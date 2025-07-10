using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.TechoModels
{
    public class TechoModel
    {
        public long Id { get; set; }
        public long? MaterialidadId { get; set; }
        public long? AislacionId { get; set; }
        public double? superficie { get; set; }
        public long? EspesorId { get; set; }
    }
}
