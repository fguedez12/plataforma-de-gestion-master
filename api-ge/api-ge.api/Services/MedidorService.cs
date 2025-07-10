using api_gestiona.DTOs.Medidor;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class MedidorService : IMedidorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MedidorService(ApplicationDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<MedidorDTO>> GetAll(MedidorFilterDTO medidorFilterDTO)
        {
            var query = _context.Medidores.AsQueryable();
            if(medidorFilterDTO.Chilemedido != null) 
            {
                query = query.Where(x => x.Chilemedido == medidorFilterDTO.Chilemedido);
            }
            if (medidorFilterDTO.MedidorConsumo != null)
            {
                query = query.Where(x => x.MedidorConsumo == medidorFilterDTO.MedidorConsumo);
            }
            if(medidorFilterDTO.DeviceId != null) 
            {
                query = query.Where(x=>x.DeviceId == medidorFilterDTO.DeviceId);
            }
            var list = await query.ToListAsync();
            var resp = _mapper.Map<List<MedidorDTO>>(list);
            return resp;
        }


    }
}
