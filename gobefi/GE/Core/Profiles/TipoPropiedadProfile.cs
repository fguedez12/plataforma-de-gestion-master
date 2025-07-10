using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TipoPropiedadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class TipoPropiedadProfile : Profile
    {
        public TipoPropiedadProfile()
        {
            CreateMap<TipoPropiedad, TipoPropiedadModel>();
            CreateMap<TipoPropiedadModel, TipoPropiedad>();
        }
    }
}
