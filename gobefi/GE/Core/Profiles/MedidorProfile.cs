using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MedidorModels;

namespace GobEfi.Web.Core.Profiles
{
    public class MedidorProfile : Profile
    {
        public MedidorProfile()
        {
            CreateMap<Medidor, MedidorModel>()
                            .ForMember(dest => dest._inteligente, opt => opt.MapFrom(x => x.Smart))
                            .ForMember(dest => dest._compartido, opt => opt.MapFrom(x => x.Compartido))
                            .ForMember(dest => dest._factura, opt => opt.MapFrom(x => x.Factura))
                            .ForMember(dest => dest.NumFases, opt => opt.MapFrom(x => x.Fases))
                            .ForMember(dest => dest.DivisionNombre, opt => opt.MapFrom(x => x.Division.Direccion))
                            .ForMember(dest => dest.ServicioNombre, opt => opt.MapFrom(x => x.Division.Servicio.Nombre))
                            .ForMember(dest => dest.InstitucionNombre, opt => opt.MapFrom(x => x.Division.Servicio.Institucion.Nombre))
                            ;

           

            CreateMap<MedidorModel, Medidor>()
                            .ForMember(dest => dest.Smart, opt => opt.MapFrom(x => x._inteligente))
                            .ForMember(dest => dest.Compartido, opt => opt.MapFrom(x => x._compartido))
                            .ForMember(dest => dest.Factura, opt => opt.MapFrom(x => x._factura))
                            .ForMember(dest => dest.Fases, opt => opt.MapFrom(x => x.NumFases));

            CreateMap<Medidor, MedidorDataModel>();
        }
    }
}
