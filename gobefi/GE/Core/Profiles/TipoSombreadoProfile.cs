using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TipoSombreadoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class TipoSombreadoProfile : Profile
    {
        public TipoSombreadoProfile()
        {
            CreateMap<TipoSombreadoModel, TipoSombreado>();
        }
    }
}
