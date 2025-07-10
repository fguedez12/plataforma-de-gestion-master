using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.UsuarioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioModel>()
                .ForMember(dest => dest.NumeroTelefono, opt => opt.MapFrom(x => x.PhoneNumber))
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(x => x.Comuna.RegionId))
                .ForMember(dest => dest.ProvinciaId, opt => opt.MapFrom(x => x.Comuna.ProvinciaId));


            CreateMap<Usuario, UsuarioListModel>();
            CreateMap<UsuarioListModel, Usuario>();

            CreateMap<UsuarioModel, Usuario>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(x => x.NumeroTelefono));

            //CreateMap<Usuario, UsuarioListExcelModel>()
            //    .ForMember(dest => dest.NumeroTelefono, opt => opt.MapFrom(x => string.IsNullOrEmpty(x.PhoneNumber) ? "" : x.PhoneNumber ))
            //    .ForMember(dest => dest.Rut, opt => opt.MapFrom(x => string.IsNullOrEmpty(x.Rut) ? "" : x.Rut))
            //    .ForMember(dest => dest.Sexo, opt => opt.MapFrom(x => x.SexoId > 0 ? x.Sexo.Nombre : ""))
            //    .ForMember(dest => dest.Region, opt => opt.MapFrom(x => x.ComunaId > 0 ? x.Comuna.Region.Nombre : ""))
            //    .ForMember(dest => dest.Certificado, opt => opt.MapFrom(x => x.Certificado ?? false ? "Si" : "No"))
            //    .ForMember(dest => dest.Validado, opt => opt.MapFrom(x => x.Validado ?? false ? "Si" : "No"));
        }
    }
}
