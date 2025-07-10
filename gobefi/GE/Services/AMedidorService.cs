using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.AMedidorModels;
using GobEfi.Web.Models.CertificadoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class AMedidorService : IAMedidorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly Usuario _currentUser;

        public AMedidorService(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;

        }

        public async Task<MedidorPaginModel> ListMedidores(int? filtroServicio,int page,int? id)
        {
            try
            {
                if (page == 0)
                {
                    page = 1;
                }
                var query =  _context.Medidores.Include(m => m.Division)
                                .ThenInclude(d=>d.Servicio)
                                .Include(m => m.NumeroCliente)
                                //.Where(m=>m.Active)
                                .OrderBy(m=>m.Id);

                if (id != null) 
                {
                    query = query.Where(x=>x.Id == id)
                             .Include(m => m.Division)
                                .Include(m => m.NumeroCliente)
                                        //.Where(m => m.Active)
                                        .OrderBy(m => m.Id);

                    page = 0;
                }

                if (filtroServicio != null)
                {
                    query = query.Where(m=>m.Division.Servicio.Id==filtroServicio)
                            .Include(m => m.Division)
                            .Include(m => m.NumeroCliente)
                                    //.Where(m => m.Active)
                                    .OrderBy(m => m.Id);
                }

                var list = page == 0 ? null : await PagingList.CreateAsync(query, 5, page);
                var listToReturn = page == 0 ? _mapper.Map<List<MedidorModel>>(await query.ToListAsync()) : _mapper.Map<List<MedidorModel>>(list);
                var ministeriosFromDb = await _context.Instituciones.ToListAsync();
                var ministeriosList = new List<MinisterioToListModel>();
                ministeriosList = _mapper.Map<List<MinisterioToListModel>>(ministeriosFromDb);
                var serviciosList = new List<ServicioToListModel>();
                var serviciosFromDb = await _context.Servicios.ToListAsync();
                serviciosList = _mapper.Map<List<ServicioToListModel>>(serviciosFromDb);


                var response = new MedidorPaginModel()
                {
                    Medidores = listToReturn,
                    PageIndex = page == 0 ? 0 : list.PageIndex,
                    StartPageIndex = page == 0 ? 0 : list.StartPageIndex,
                    StopPageIndex = page == 0 ? 0 : list.StopPageIndex,
                    Ministerios = ministeriosList,
                    Servicios = serviciosList
                };

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateMedidor(MedidorModel model) 
        {
            try
            {
                var medidorFromDb = await _context.Medidores.Where(m => m.Id == model.Id).FirstOrDefaultAsync();

                medidorFromDb.DivisionId = model.DivisionId;
                medidorFromDb.Numero = model.Numero;
                medidorFromDb.Active = model.Active;
                medidorFromDb.UpdatedAt = DateTime.Now;
                medidorFromDb.ModifiedBy = _currentUser.Id;
                medidorFromDb.Version = medidorFromDb.Version + 1;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           
        }
    }

}
