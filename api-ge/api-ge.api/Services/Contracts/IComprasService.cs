using api_gestiona.DTOs.Compras;

namespace api_gestiona.Services.Contracts
{
    public interface IComprasService
    {
        Task<InsertCompraResponseDTO> InsertCompra(CompraToSaveDTO compra);
        Task DeleteCompra(long id);
    }
}
