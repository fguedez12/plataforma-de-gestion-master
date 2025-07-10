using System.Threading.Tasks;
using GobEfi.Web.Data.Entities;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface ICompraMedidorRepository : IRepository<CompraMedidor, long>
    {
        Task<bool> SaveAll();
        void EliminarSegunCompraId(long compraId);
    }
}