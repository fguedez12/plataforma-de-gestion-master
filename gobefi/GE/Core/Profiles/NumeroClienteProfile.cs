using AutoMapper;
using GE.Models.NumeroClienteModels;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.NumeroClienteModels;

namespace GobEfi.Web.Core.Profiles
{
    public class NumeroClienteProfile : Profile
    {
        public NumeroClienteProfile()
        {
            CreateMap<NumeroCliente, NumeroClienteModel>();
            CreateMap<NumeroClienteModel,NumeroCliente>();

            CreateMap<NumeroCliente, NumeroClienteDataModel>();
            CreateMap<NumeroCliente, NumClienteToDDL>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.Numero));
        }
    }
}
