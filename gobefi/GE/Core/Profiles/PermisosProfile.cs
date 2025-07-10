using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.PermisosModels;

namespace GobEfi.Web.Core.Profiles
{
    public class PermisosProfile : Profile
    {
        public PermisosProfile()
        {
            CreateMap<Permisos, PermisosModel>();
            CreateMap<PermisosModel, Permisos>();
        }
    }
}