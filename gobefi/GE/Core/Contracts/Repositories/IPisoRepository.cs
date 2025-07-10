using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IPisoRepository : IRepository<Piso, long>
    {
        void Add<T>(T entity) where T : class;

        Task<bool> SaveAll();
    }
}
