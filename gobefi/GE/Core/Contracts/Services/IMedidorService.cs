using GE.Models.MedidorModels;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.MedidorModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IMedidorService :  IService<MedidorModel, long>
    {
        //IEnumerable<MedidorModel> Get(long divisionId, long EnergeticoDivisionId);

        /// <summary>
        /// Segun el numero de cliente "Id" obtiene la lista de medidores y por cada item de la lista llama al metodo Delete(Id) pasando como parametro
        /// el id de cada Item
        /// </summary>
        /// <param name="numClienteId">Id del numero de cliente para buscar los medidores asociados</param>
        void DeleteByNumClienteId(long id);
        List<MedidorModel> GetByNumClienteId(long id);

        /// <summary>
        /// Obtiene una lista de medidores que tengan el mismo Numero de Medidor
        /// </summary>
        /// <param name="numMedidor"></param>
        /// <returns></returns>
        List<MedidorModel> GetByNumMedidor(long numMedidor);

        List<MedidorModel> GetByDivisionId(long divisionId);
        Task<IEnumerable<MedidorToDDL>> ByNumeroCliente(long ByNumeroClienteId);
        Task<MedidorModel> GetByNumClienteIdAndNumMedidor(MedidorParametrosModel parametroMedidor);

        bool EstanAsociado(long medidorId, long divisionId);
        Task<IEnumerable<MedidorToDDL>> ByNumClienteIdDivisionId(long numClienteId, long divisionId);
        Task<IEnumerable<MedidorToDDL>> GetByCompraId(long compraId);
        void DesasociarbyNumCliente(long numClienteId, long divisionId);
    }
}
