using GobEfi.Web.Core.Metadata;
using GobEfi.Web.Models.ArchivoAdjuntoModels;
using GobEfi.Web.Models.ComponenteModels;
using GobEfi.Web.Models.CompraModels;
using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.EquipoModels;
using GobEfi.Web.Models.MedidorModels;
using GobEfi.Web.Models.NumeroClienteModels;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Models.SubDivisionModels;
using GobEfi.Web.Models.TipoAgrupamientoModels;
using GobEfi.Web.Models.TipoPropiedadModels;
using GobEfi.Web.Models.TipoUnidadModels;
using GobEfi.Web.Models.TipoUsoModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DivisionModels
{
    [ModelMetadataType(typeof(DivisionMetadata))]
    public class DivisionModel : BaseModel<long>
    {
        public string Direccion { get; set; }
        public int Funcionarios { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        public bool ReportaPMG { get; set; }

        [Required]
        [Display(Name = "Año de Construcción")]
        public int AnyoConstruccion { get; set; }
        public string Pisos { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Superficie { get; set; }

        [Display(Name = "Edificio")]
        public long EdificioId { get; set; }

        [Display(Name = "Servicio")]
        public long ServicioId { get; set; }

        [Display(Name = "Tipo de Unidad")]
        public long TipoUnidadId { get; set; }

        [Display(Name = "Tipo de Agrupamiento")]
        public long TipoAgrupamientoId { get; set; }

        [Display(Name = "Tipo de Propiedad")]
        public long TipoPropiedadId { get; set; }

        [Display(Name = "Tipo de Uso")]
        public long? TipoUsoId { get; set; }

        public bool PisosIguales { get; set; }

        [Display(Name = "Edificio")]
        public virtual EdificioModel Edificio { get; set; }

        [Display(Name = "Servicio")]
        public virtual ServicioModel Servicio { get; set; }

        [Display(Name = "Tipo de Unidad")]
        public virtual TipoUnidadModel TipoUnidad { get; set; }

        [Display(Name = "Tipo de Agrupamiento")]
        public virtual TipoAgrupamientoModel TipoAgrupamiento { get; set; }

        [Display(Name = "Tipo de Propiedad")]
        public virtual TipoPropiedadModel TipoPropiedad { get; set; }

        [Display(Name = "Tipo de Uso")]
        public virtual TipoUsoModel TipoUso { get; set; }

        public ICollection<ComponenteModel> Componentes { get; set; }
        public ICollection<EquipoModel> Equipos { get; set; }
        public ICollection<ArchivoAdjuntoModel> Adjuntos { get; set; }
        public ICollection<SubDivisionModel> SubDivisiones { get; set; }
        public ICollection<NumeroClienteModel> NumerosClientes { get; set; }
        public ICollection<MedidorModel> Medidores { get; set; }
        public ICollection<CompraModel> Compras { get; set; }
        //public ICollection<EnergeticoModel> Energeticos { get; set; }
    }
}
