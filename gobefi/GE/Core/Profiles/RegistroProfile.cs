using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class RegistroProfile : Profile
    {
        public RegistroProfile()
        {
            CreateMap<RegistroViewModel, Registro>();
        }
    }
}
