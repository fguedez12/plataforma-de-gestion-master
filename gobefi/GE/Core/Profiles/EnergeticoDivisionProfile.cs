using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoDivisionModels;

namespace GobEfi.Web.Core.Profiles
{
    public class EnergeticoDivisionProfile : Profile
    {
        public EnergeticoDivisionProfile()
        {
            CreateMap<EnergeticoDivision, EnergeticoDivisionModel>();

            CreateMap<EnergeticoDivisionModel, EnergeticoDivision>();
        }
    }
}
