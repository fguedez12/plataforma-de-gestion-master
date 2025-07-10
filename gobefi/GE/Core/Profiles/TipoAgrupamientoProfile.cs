using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TipoAgrupamientoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class TipoAgrupamientoProfile : Profile
    {
        public TipoAgrupamientoProfile()
        {
            CreateMap<TipoAgrupamiento, TipoAgrupamientoModel>();
            CreateMap<TipoAgrupamientoModel, TipoAgrupamiento>();
        }
    }
}
