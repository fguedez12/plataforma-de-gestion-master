using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Shared.Entities
{
    public class Division : BaseEntity
    {
        public int Funcionarios { get; set; }
        public string Nombre { get; set; }
        public string Pisos { get; set; }
        public bool ReportaPMG { get; set; }
        public int AnyoConstruccion { get; set; }
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public double? Superficie { get; set; }
        public long EdificioId { get; set; }
        public long ServicioId { get; set; }
        public long? TipoUnidadId { get; set; }
        public long TipoPropiedadId { get; set; }
        public long? TipoUsoId { get; set; }
        [MaxLength(255)]
        public string NroRol { get; set; }
        public string Direccion { get; set; }
        public bool ComparteMedidorElectricidad { get; set; }
        public bool ComparteMedidorGasCanieria { get; set; }

        public ICollection<MedidorInteligenteDivision> MedidorInteligenteDivisiones { get; set; }
        public ICollection<MedidorDivision> MedidorDivision { get; set; }
    }
}
