using api_gestiona.DTOs.Compras;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Kiota.Abstractions;

namespace api_gestiona.Services
{
    public class ComprasService:IComprasService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ComprasService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task DeleteCompra(long id)
        {
            var compraFdb = await _context.Compras.FirstOrDefaultAsync(x => x.Id == id);
            if (compraFdb == null)
            {
                throw new Exception("la compra no existe");
            }

            compraFdb.Active = false;
            compraFdb.UpdatedAt = DateTime.Now;
            compraFdb.ModifiedBy = "System";
            compraFdb.Version = compraFdb.Version + 1;
            _context.Update(compraFdb);
            await _context.SaveChangesAsync();

        }
        public async Task<InsertCompraResponseDTO> InsertCompra(CompraToSaveDTO compra)
        {
            var numeroClienteExists = await _context.NumeroClientes.AnyAsync(x => x.Id == compra.NumeroClienteId);
            var response = new InsertCompraResponseDTO();
            if (!numeroClienteExists)
            {
                throw new Exception("Numero de cliente no existe");
            }
            var medidorExists = await _context.Medidores.AnyAsync(x => x.Id == compra.MedidorId);
            if (!medidorExists)
            {
                throw new Exception("Medidor no existe");
            }
            var divisionExist = await _context.Divisiones.AnyAsync(x => x.Id == compra.DivisionId);
            if (!divisionExist)
            {
                throw new Exception("Unidad no existe");
            }
            try
            {
                var entity = _mapper.Map<Compra>(compra);
                entity.CreatedAt = DateTime.Now;
                entity.Active = true;
                entity.Version = 1;
                entity.CreatedBy = compra.UserId;
                await _context.Compras.AddAsync(entity);
                await _context.SaveChangesAsync();
                response.IdCompra = entity.Id;
                if (compra.MedidorId != null)
                {
                    var cm = new CompraMedidor
                    {
                        CompraId = entity.Id,
                        MedidorId = compra.MedidorId.Value,
                        Consumo = compra.Consumo,
                        ParametroMedicionId = 1
                    };
                    await _context.CompraMedidor.AddAsync(cm);
                    await _context.SaveChangesAsync();
                    response.IdConsumo = cm.Id;
                }
                return response;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
