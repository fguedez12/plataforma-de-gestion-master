using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.AMedidorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class AMedidorProfile : Profile
    {
        public AMedidorProfile()
        {
            CreateMap<Medidor, MedidorModel>()
              .ForMember(dest => dest.Division, opt => opt.MapFrom(x=>x.Division.Nombre))
              .ForMember(dest => dest.NumeroCliente, opt => opt.MapFrom(x => x.NumeroCliente.Numero))
              .ForMember(dest => dest.EsCompartido, opt => opt.MapFrom(x => x.Compartido))
              .ForMember(dest => dest.EsInteligente, opt => opt.MapFrom(x => x.Smart));
        }
    }
}
