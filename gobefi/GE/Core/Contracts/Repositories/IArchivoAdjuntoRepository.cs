using System.Threading.Tasks;
using GobEfi.Web.Data.Entities;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IArchivoAdjuntoRepository : IRepository<ArchivoAdjunto, long>
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();
    }
}