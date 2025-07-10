using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.VentanaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class VentanaProfile : Profile
    {
        public VentanaProfile()
        {
            CreateMap<VentanaModel, Ventana>();
            CreateMap<Ventana, VentanaModel>();
        }
    }
}
