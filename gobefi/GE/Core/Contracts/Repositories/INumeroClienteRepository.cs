using GobEfi.Web.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface INumeroClienteRepository : IRepository<NumeroCliente, long>
    {
        //IQueryable<NumeroCliente> Get(long divisionId, long energeticoDivisionId);
        NumeroCliente GetByNumero(string numeroCliente);
        bool NumClientExist(string numeroCliente, long divisionId, long energeticoDivisionId);
    }
}
