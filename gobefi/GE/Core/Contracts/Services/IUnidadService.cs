
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.UnidadesDTO;
using GobEfi.Web.Models.UnidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IUnidadService
    {
        Task<bool> CheckUnidadNombre(string nombre, long servicioId);
        Task<UnidadModel> Create(UnidadToSave model);
        Task DeleteUnidad(Unidad unidad);
        Task<UnidadDTO> Get(long unidadId);
        Task<List<UnidadListDTO>> GetAsociadosByUserId(string userId);
        Task<List<UnidadListDTO>> GetbyFilter(string userId, long? institucionId, long? servicioId, long? regionId);
        Task<bool> HasInteligentMeasurement(long unidadId);
        IQueryable<Unidad> Query();
        Task Update(long id, UnidadToUpdate model);
    }
}
