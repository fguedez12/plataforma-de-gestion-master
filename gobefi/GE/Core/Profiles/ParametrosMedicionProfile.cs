using AutoMapper;
using GE.Models.ParametrosMedicionModels;
using GobEfi.Web.Data.Entities;

namespace GobEfi.Web.Core.Profiles
{
    public class ParametrosMedicionProfile : Profile
    {
        public ParametrosMedicionProfile()
        {
            CreateMap<ParametroMedicion, ParametrosMedicionModel>()
                .ForMember(dest => dest.AbrvUnidadMedida, opt => opt.MapFrom(x => x.UnidadesMedida.Abrv))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => $"{x.Nombre} ({x.UnidadesMedida.Abrv})"));
        }
    }
}