using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CompraMedidorModels;

namespace GobEfi.Web.Core.Profiles
{
    public class CompraMedidorProfile : Profile
    {
        public CompraMedidorProfile()
        {
            CreateMap<CompraMedidorForRegister, CompraMedidor>();

            CreateMap<CompraMedidorForEdit, CompraMedidor>();

            CreateMap<CompraMedidor, CompraMedidorForValidate>()
                .ForMember(dest => dest.Medidor, opt => opt.MapFrom(x => x.Medidor.Numero));
        }
    }
}