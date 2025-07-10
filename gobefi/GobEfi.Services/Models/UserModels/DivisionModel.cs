using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Models.UserModels
{
    public class DivisionModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }
        public bool Active { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public string Nombre { get; set; }
    }
}
