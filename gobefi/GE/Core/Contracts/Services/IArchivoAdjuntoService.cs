using System.Collections.Generic;
using System.Threading.Tasks;
using GobEfi.Web.Models.ArchivoAdjuntoModels;
using GobEfi.Web.Models.TipoArchivoModels;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IArchivoAdjuntoService : IService<ArchivoAdjuntoForRegister, long>
    {
        Task<long> Add<T>(T entity) where T : class;

        long Update(ArchivoAdjuntoForEdit model);
        Task<ArchivoAdjuntoModel> GetByFacturaId(long compraId);
        bool ValidaArchivoParaCompra(string ext);
        Task<ICollection<TipoArchivoModel>> GetExtPermitidasFacturaAsync();
    }
}