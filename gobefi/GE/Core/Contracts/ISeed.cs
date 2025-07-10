using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts
{
    public interface ISeed
    {
        Task<Usuario> Seed();
    }
}
