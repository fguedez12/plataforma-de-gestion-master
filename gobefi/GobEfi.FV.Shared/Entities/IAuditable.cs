using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.FV.Shared.Entities
{
    public interface IAuditable
    {
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int Version { get; set; }
    }
}
