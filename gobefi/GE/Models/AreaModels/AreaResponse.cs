using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.AreaModels
{
    public class AreaResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public List<AreaModel> List { get; set; }
    }
}
