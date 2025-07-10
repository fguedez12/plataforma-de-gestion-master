using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MenuPanelModels;

namespace GobEfi.Web.Core.Profiles
{
    public class MenuPanelProfile : Profile
    {
        public MenuPanelProfile()
        {
            CreateMap<MenuPanel, MenuPanelModel>();
            CreateMap<MenuPanelModel, MenuPanel>();
        }
    }
}