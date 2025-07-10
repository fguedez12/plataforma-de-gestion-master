using GobEfi.Web.Models.PisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MuroModels
{
    public class MuroResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public List<PisoModel> MuroList { get; set; }
    }
}
