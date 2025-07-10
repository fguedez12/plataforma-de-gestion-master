using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MenuModels;

namespace GobEfi.Web.Core.Profiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuModel>();
            CreateMap<MenuModel, Menu>();
        }
    }
}