using AutoMapper;
using GobEfi.Business.Validaciones;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.UnidadesDTO;
using GobEfi.Web.Models.DivisionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class DivisionProfile : Profile
    {
        public DivisionProfile()
        {
            CreateMap<Division, DivisionListModel>()
                .ForMember(dest => dest.InstitucionId, opt => opt.MapFrom(x => x.Servicio.InstitucionId))
                .ForMember(dest => dest.ServicioEsPMG, opt => opt.MapFrom(x => x.Servicio.ReportaPMG));

            CreateMap<Division, DivisionVerModel>();
                //.ForMember(dest => dest.Direccion, opt => opt.MapFrom(x => $"{x.Edificio.Calle}, {x.Edificio.Numero}, {x.Pisos}, {x.Edificio.Comuna.Region.Nombre}"));


            CreateMap<Division, DivisionModel>();
            CreateMap<DivisionModel, Division>();

            CreateMap<Division, DivisionDataModel>();
            CreateMap<DivisionDataModel, Division>();

            CreateMap<DivisionEditInfGeneralModel, Division>();


            CreateMap<DivisionCreateModel, Division>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.NombreUnidad))
                .ForMember(dest => dest.TipoUsoId, opt => opt.MapFrom(x => x.TipoDeUsoId))
                .ForMember(dest => dest.AnyoConstruccion, opt => opt.MapFrom(x => x.AnioConstruccion))
                .ForMember(dest => dest.Funcionarios, opt => opt.MapFrom(x => x.NroFuncionarios));

            CreateMap<Division, DivisionDeleteModel>()
                .ForMember(dest => dest.NombreUnidad, opt => opt.MapFrom(x => x.Nombre))
                .ForMember(dest => dest.AnioConstruccion, opt => opt.MapFrom(x => x.AnyoConstruccion))
                .ForMember(dest => dest.NroFuncionarios, opt => opt.MapFrom(x => x.Funcionarios))
                .ForMember(dest => dest.TipoDeUso, opt => opt.MapFrom(x => x.TipoUso));

            CreateMap<Division, DivisionEditModel>()
                .ForMember(dest => dest.NombreUnidad, opt => opt.MapFrom(x => x.Nombre))
                .ForMember(dest => dest.AnioConstruccion, opt => opt.MapFrom(x => x.AnyoConstruccion))
                .ForMember(dest => dest.NroFuncionarios, opt => opt.MapFrom(x => x.Funcionarios))
                .ForMember(dest => dest.TipoDeUsoId, opt => opt.MapFrom(x => x.TipoUsoId))
                .ForMember(dest => dest.RegionId, opt => opt.MapFrom(x => x.Edificio.Comuna.RegionId))
                .ForMember(dest => dest.ProvinciaId, opt => opt.MapFrom(x => x.Edificio.Comuna.ProvinciaId))
                .ForMember(dest => dest.ComunaId, opt => opt.MapFrom(x => x.Edificio.ComunaId))
                .ForMember(dest => dest.InstitucionId, opt => opt.MapFrom(x => x.Servicio.InstitucionId));


            CreateMap<DivisionEditModel, Division>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(x => x.NombreUnidad))
                .ForMember(dest => dest.AnyoConstruccion, opt => opt.MapFrom(x => x.AnioConstruccion))
                .ForMember(dest => dest.Funcionarios, opt => opt.MapFrom(x => x.NroFuncionarios))
                .ForMember(dest => dest.TipoUsoId, opt => opt.MapFrom(x => x.TipoDeUsoId));


            CreateMap<Division, DivisionesToAssociate>()
                .ForMember(dest => dest.NombreEdificio, opt => opt.MapFrom(x => x.Edificio.Direccion))
                .ForMember(dest => dest.NombreUnidad, opt => opt.MapFrom(x => x.Nombre))
                .ForMember(dest => dest.NombreRegion, opt => opt.MapFrom(x => x.Edificio.Comuna.Region.Nombre));

            CreateMap<Division, DivisionDetailsModel>()
                .ForMember(dest => dest.NombreUnidad, opt => opt.MapFrom(x => x.Nombre))
                .ForMember(dest => dest.TipoDeUsoId, opt => opt.MapFrom(x => x.TipoUso.Id))
                .ForMember(dest => dest.AnioConstruccion, opt => opt.MapFrom(x => x.AnyoConstruccion))
                .ForMember(dest => dest.ComunaId, opt => opt.MapFrom(x => x.Edificio.ComunaId))
                .ForMember(dest => dest.InstitucionId, opt => opt.MapFrom(x => x.Servicio.InstitucionId))
                .ForMember(dest => dest.NroFuncionarios, opt => opt.MapFrom(x => x.Funcionarios))
                .ForMember(dest => dest.TipoPropiedadId, opt => opt.MapFrom(x => x.TipoPropiedad.Id));
            CreateMap<Division, UnidadReporteConsumoDTO>();

        }
    }
}
