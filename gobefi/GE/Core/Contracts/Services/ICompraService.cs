using GobEfi.Web.Models.CompraModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface ICompraService : IService<CompraModel,long>
    {
        Task<long> Add<T>(T entity) where T : class;
        Task<long> Update(long id, CompraForEdit compraFromBody);
        Task<int> DeleteAsync(long id);
        Task<bool> ValidaPermiso(long divisionId);
        Task<IEnumerable<CompraParaValidarModel>> getComprasParaValidar(CompraParaValidarRequest request);
        Task<CompraParaValidarDetalleModel> GetParaValidar(long compraId);
        List<CompraTablaEnergetico> ObtenerLasCompras(long divisionId, List<EnergeticoActivoModel> energeticos, int? anioFiltro);
        Task<string> AccionEstado(long compraId, string accion, string obs);
        Task<CompraForEdit> GetParaRetornar(long compraId);
    }
}
