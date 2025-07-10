using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TipoUnidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class TipoUnidadProfile : Profile
    {
        public TipoUnidadProfile()
        {
            CreateMap<TipoUnidad, TipoUnidadModel>();
            CreateMap<TipoUnidadModel, TipoUnidad>();
        }
    }
}
