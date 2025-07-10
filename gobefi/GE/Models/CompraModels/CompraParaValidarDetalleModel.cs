using GobEfi.Web.Models.CompraMedidorModels;
using System;
using System.Collections.ObjectModel;

namespace GobEfi.Web.Models.CompraModels
{
    public class CompraParaValidarDetalleModel
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ModifiedBy { get; set; }
        //public string EnergeticoName { get; set; }
        public long EnergeticoId { get; set; }
        public string FechaCompra { get; set; }
        public string InicioLectura { get; set; }
        public string FinLectura { get; set; }
        public long? NumeroClienteId { get; set; }
        public string NumeroCliente { get; set; }
        public Collection<CompraMedidorForValidate> ListaMedidores { get; set; }
        public double Costo { get; set; }
        public double Consumo { get; set; }
        public double ConsumoCompra { get; set; }
        public string TextUnidadMedida { get; set; }
        public long FacturaId { get; set; }
        //public IFormFile Factura { get; set; }
        //public long DivisionId { get; set; }
        public string Observacion { get; set; }
        //public string CreadoEn { get;set; }
        //public string ActualizadoEn { get; set; }
        //public long? UnidadMedidaId { get; set; }
        public string EstadoValidacion { get; set; }
        public string RevisadoPor { get; set; }

        public bool TieneConsumos { get; set; }

        public DateTime? ReviewedAt { get; set; }
        public EnergeticoModels.EnergeticoActivoModel Energetico { get; set; }

        public string ObservacionRevision { get; set; }
    }
}
