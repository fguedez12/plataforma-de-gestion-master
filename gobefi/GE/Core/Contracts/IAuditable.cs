using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts
{
    public interface IAuditable
    {
        long Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        long Version { get; set; }
        bool Active { get; set; }
        string ModifiedBy { get; set; }
        string CreatedBy { get; set; }
    }
}
