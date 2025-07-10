using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MuroModels
{
    public class MuroMaterialidadModel
    {
        public long? MaterialidadId { get; set; }
        public long? AislacionIntId { get; set; }
        public long? AislacionExtId { get; set; }
        public double? Superficie { get; set; }
    }
}
