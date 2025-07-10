using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EmpresaDistribuidoraModels;

namespace GobEfi.Web.Core.Profiles
{
    public class EmpresaDistribuidoraProfile : Profile
    {
        public EmpresaDistribuidoraProfile()
        {
            CreateMap<EmpresaDistribuidora, EmpresaDistribuidoraModel>();
                

            CreateMap<EmpresaDistribuidora, EmpresaDistribuidoraModel>();
            CreateMap<EmpresaDistribuidoraModel, EmpresaDistribuidora>();

            CreateMap<EmpresaDistribuidora, EmpresaDistribuidoraListModel>()
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(x => x.Active));


        }
    }
}
