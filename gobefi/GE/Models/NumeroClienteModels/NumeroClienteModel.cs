using GobEfi.Web.Models.EmpresaDistribuidoraModels;
using GobEfi.Web.Models.TipoTarifaModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Models.NumeroClienteModels
{
    public class NumeroClienteModel : BaseModel<long>
    {
        [DisplayName("Número:")]
        public string Numero { get; set; }
        public string NombreCliente { get; set; }
        public long? EmpresaDistribuidoraId { get; set; }
        public long? TipoTarifaId { get; set; }
        [Range(0,long.MaxValue, ErrorMessage = "Potencia suministrada debe ser un valor mayor a 0.")]
        public decimal PotenciaSuministrada { get; set; }
        public string Potencia { get; set; }
        public long MedicionPotencia { get; set; }
        public long MedicionReactivo { get; set; }

        public int NumMedidoresExclusivos { get; set; }
        public int NumMedidoresCompartidos { get; set; }
        public int TotalMedidores { get; set; }
        public long EnergeticoId { get; set; }
        public EmpresaDistribuidoraModel EmpresaDistribuidora { get; set; }
        public TipoTarifaModel TipoTarifa { get; set; }
    }
}
