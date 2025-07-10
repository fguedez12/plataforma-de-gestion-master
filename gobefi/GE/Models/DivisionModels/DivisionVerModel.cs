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
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DivisionModels
{
    public class DivisionVerModel : DivisionBaseModel
    {
        public int Funcionarios { get; set; }
        public int NroOtrosColaboradores { get; set; }
        [Display(Name = "Nombre (opcional)")]
        public string Nombre { get; set; }
        [DisplayName("Se incluye  para reporte consumo energía del PMG")]
        public bool ReportaPMG { get; set; }
        [DisplayName("Se incluye para Indicador EE del PMG")]
        public bool IndicadorEE { get; set; }
        [DisplayName("Se incluye para reporte de gestión ambiental del PMG")]
        public bool ReportaEV { get; set; }
        [Required]
        [Display(Name = "Año de Construcción")]
        public int AnyoConstruccion { get; set; }
        public string Pisos { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Superficie { get; set; }
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
        public long TipoUsoId { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Display(Name = "")]
        public bool ComparteMedidorElectricidad { get; set; }
        [Display(Name = "")]
        public bool ComparteMedidorGasCanieria { get; set; }
        public string NroRol { get; set; }
        public bool DisponeVehiculo { get; set; }
        public bool DisponeCalefaccion { get; set; }
        public bool AireAcondicionadoElectricidad { get; set; }
        public bool CalefaccionGas { get; set; }
        [DisplayName("Institución responsable de los consumos")]
        public int? InstitucionResponsableId { get; set; }
        [DisplayName("Servicio responsable de los consumos")]
        public int? ServicioResponsableId { get; set; }
        [DisplayName("Organización responsable de los consumos")]
        public string OrganizacionResponsable { get; set; }
        public bool TieneMedidorElectricidad { get; set; }
        public bool TieneMedidorGas { get; set; }
        [DisplayName("Tiene acceso a las facturas de Agua")]
        public int? AccesoFacturaAgua { get; set; }
        [DisplayName("Institución responsable de los consumos")]
        public int? InstitucionResponsableAguaId { get; set; }
        [DisplayName("Servicio responsable de los consumos")]
        public int? ServicioResponsableAguaId { get; set; }
        [DisplayName("Organización responsable de los consumos")]
        public string OrganizacionResponsableAgua { get; set; }
        public bool ComparteMedidorAgua { get; set; }
        public string VehiculosIds { get; set; }
        public string Patentes { get; set; }


        public virtual EdificioModel Edificio { get; set; }
        public virtual ServicioModel Servicio { get; set; }
        public virtual TipoUnidadModel TipoUnidad { get; set; }
        public virtual TipoAgrupamientoModel TipoAgrupamiento { get; set; }
        public virtual TipoPropiedadModel TipoPropiedad { get; set; }
        public virtual TipoUsoModel TipoUso { get; set; }

        public ICollection<ComponenteModel> Componentes { get; set; }
        public ICollection<EquipoModel> Equipos { get; set; }
        public ICollection<ArchivoAdjuntoModel> Adjuntos { get; set; }
        public ICollection<SubDivisionModel> SubDivisiones { get; set; }
        public ICollection<NumeroClienteModel> NumerosClientes { get; set; }
        public ICollection<MedidorModel> Medidores { get; set; }
        public ICollection<CompraModel> Compras { get; set; }
        //public ICollection<EnergeticoDivisionModel> Energeticos { get; set; }

        public SelectList TipoUsos { get; set; }
        public SelectList TipoPropiedades { get; set; }
        public int AccesoFactura { get; set; }

        public string AccesoFacturaString {
            get
            {
                
                switch (AccesoFactura)
                {
                   
                    case 1:
                        return "Si";
                    case 2:
                        return "No, Otro servicio público";
                    case 3:
                        return "No, Otra organización";
                    default:
                        return "Sin información";
                }
            }
        }

        public string Responsable { get; set; }
    }
}
