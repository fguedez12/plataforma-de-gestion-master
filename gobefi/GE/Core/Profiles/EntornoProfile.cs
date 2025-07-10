using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EntornoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class EntornoProfile : Profile
    {
        public EntornoProfile() {
            CreateMap<Entorno, EntornoModel>();
            CreateMap<EntornoModel, Entorno>();
        }
    }
}
