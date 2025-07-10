using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoModels;

namespace GobEfi.Web.Core.Profiles
{
    public class EnergeticoProfile : Profile
    {
        public EnergeticoProfile()
        {
            CreateMap<Energetico, EnergeticoListModel>()
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(x => x.Active));



            CreateMap<Energetico, EnergeticoModel>()
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(x => x.Active));

            CreateMap<EnergeticoModel, Energetico>();
            CreateMap<EnergeticoListModel, Energetico>();

            CreateMap<Energetico, EnergeticoDataModel>();
            CreateMap<EnergeticoDataModel, Energetico>();

            CreateMap<Energetico, EnergeticoForEditModel>()
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(x => x.Active));

            CreateMap<EnergeticoForEditModel, Energetico>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(x => x.Activo));

            CreateMap<Energetico, EnergeticoActivoModel>();
        }
    }
}
