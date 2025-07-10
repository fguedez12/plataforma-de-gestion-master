using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TipoArchivoModels;

namespace GobEfi.Web.Core.Profiles
{
    public class TipoArchivoProfile : Profile
    {
        public TipoArchivoProfile()
        {
            CreateMap<TipoArchivo, TipoArchivoModel>();
            CreateMap<TipoArchivoModel, TipoArchivo>();
        }
    }
}
