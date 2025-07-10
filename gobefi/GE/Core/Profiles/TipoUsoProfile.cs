using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.TipoUsoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class TipoUsoProfile : Profile
    {
        public TipoUsoProfile()
        {
            CreateMap<TipoUso, TipoUsoModel>();
            CreateMap<TipoUsoModel, TipoUso>();
        }
    }
}
