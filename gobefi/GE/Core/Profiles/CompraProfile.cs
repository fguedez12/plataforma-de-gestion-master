using AutoMapper;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CompraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Profiles
{
    public class CompraProfile : Profile
    {
        public CompraProfile()
        {
            CreateMap<Compra, CompraForRegister>();
            CreateMap<CompraForRegister, Compra>()
                .ForMember(dest => dest.CompraMedidor, opt => opt.MapFrom(x => x.ListaMedidores))
                .ForMember(dest => dest.Consumo, opt => opt.MapFrom(x => x.ConsumoCompra));


            CreateMap<Compra, CompraForEdit>()
                .ForMember(dest => dest.TextUnidadMedida, opt => opt.MapFrom(x => x.UnidadMedida.Abrv))
                .ForMember(dest => dest.ListaMedidores, opt => opt.MapFrom(x => x.CompraMedidor))
                //.ForMember(dest => dest.EstadoValidacion, opt => opt.MapFrom(x => x.EstadoValidacion == "sin_revision" ? "Sin Revision" : x.EstadoValidacion == "ok" ? "Ok" : "Observado"));
                .ForMember(dest => dest.EstadoValidacion, opt => opt.MapFrom(x => x.EstadoValidacion.Nombre));

            CreateMap<Compra, CompraParaValidarDetalleModel>()
               .ForMember(dest => dest.TextUnidadMedida, opt => opt.MapFrom(x => x.UnidadMedida.Abrv))
               .ForMember(dest => dest.ListaMedidores, opt => opt.MapFrom(x => x.CompraMedidor))
               .ForMember(dest => dest.EstadoValidacion, opt => opt.MapFrom(x => x.EstadoValidacion.Nombre))
               //.ForMember(dest => dest.Energetico, opt => opt.MapFrom(x => x.Energetico.Nombre))
               .ForMember(dest => dest.NumeroCliente, opt => opt.MapFrom(x => x.NumeroCliente.Numero));

            CreateMap<CompraForEdit, Compra>()
                .ForMember(dest => dest.CompraMedidor, opt => opt.MapFrom(x => x.ListaMedidores));

            CreateMap<Compra, CompraModel>();
            CreateMap<CompraModel, Compra>();

            CreateMap<Compra, CompraTablaEnergetico>()
                .ForMember(dest => dest.AnioCompra, opt => opt.MapFrom(x => x.FechaCompra.Year))
                .ForMember(dest => dest.AnioFechaInicio, opt => opt.MapFrom(x => x.InicioLectura.Year))
                .ForMember(dest => dest.AnioFechaFin, opt => opt.MapFrom(x => x.FinLectura.Year))
                .ForMember(dest => dest.Abrev, opt => opt.MapFrom(x => x.UnidadMedida.Abrv))
                .ForMember(dest => dest.NumeroCliente, opt => opt.MapFrom(x => x.NumeroCliente.Numero))
                .ForMember(dest => dest.Energetico, opt => opt.MapFrom(x => x.Energetico.Nombre))
                .ForMember(dest => dest.Costo, opt => opt.MapFrom(x => Convert.ToInt64(x.Costo)))
                //.ForMember(dest => dest.Estado, opt => opt.MapFrom(x => x.EstadoValidacionId == "sin_revision" ? "Sin Revision" : x.EstadoValidacionId == "ok" ? "Ok" : "Observado"));
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(x => x.EstadoValidacion.Nombre));


            //CreateMap<Compra, CompraParaValidarModel>()
            //    .ForMember(dest => dest.InicioDeLectura, opt => opt.MapFrom(x => x.InicioLectura.ToShortDateString()))
            //    .ForMember(dest => dest.Unidad, opt => opt.MapFrom(x => x.Division.Direccion))
            //    .ForMember(dest => dest.Energetico, opt => opt.MapFrom(x => x.Energetico.Nombre))
            //    .ForMember(dest => dest.NumCliente, opt => opt.MapFrom(x => x.NumeroCliente.Numero))
            //    .ForMember(dest => dest.Estado, opt => opt.MapFrom(x => x.EstadoValidacion.Nombre))
            //    .ForMember(dest => dest.EstadoId, opt => opt.MapFrom(x => x.EstadoValidacion.Id));
        }
    }
}
