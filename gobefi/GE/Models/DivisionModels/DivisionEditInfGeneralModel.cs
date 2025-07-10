using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Models.DivisionModels
{
    public class DivisionEditInfGeneralModel : DivisionBaseModel
    {
        [Range(1, double.MaxValue, ErrorMessage = "Debe ingresar un valor mayor a cero")]
        public int Funcionarios { get; set; }
        public string Nombre { get; set; }


        // 13/06/2019 se detecto un error al editar por informacion general
        // este parametro es desactivado 
        public bool ReportaPMG { get; set; } 
        public int AnyoConstruccion { get; set; }
        public string Pisos { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Debe ingresar un valor mayor a cero")]
        public double Superficie { get; set; }
        public long EdificioId { get; set; }
        public long ServicioId { get; set; }
        public long? TipoUnidadId { get; set; }
        public long? TipoAgrupamientoId { get; set; }
        public long TipoPropiedadId { get; set; }
        public long? TipoUsoId { get; set; }
        public string NroRol { get; set; }

        public bool ComparteMedidorElectricidad { get; set; }
        public bool ComparteMedidorGasCanieria { get; set; }
        public bool ReportaIndicadorEfiEnergetica { get; set; }
    }
}
