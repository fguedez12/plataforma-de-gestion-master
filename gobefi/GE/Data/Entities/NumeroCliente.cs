using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class NumeroCliente : BaseEntity
    {
        public string Numero { get; set; }
        public string NombreCliente { get; set; }
        public decimal PotenciaSuministrada { get; set; }
        public long? EmpresaDistribuidoraId { get; set; }
        public long? TipoTarifaId { get; set; }
        public EmpresaDistribuidora EmpresaDistribuidora { get; set; }
        public TipoTarifa TipoTarifa { get; set; }
        public ICollection<Compra> Compras { get; set; }
        public ICollection<Medidor> Medidores { get; set; }
        public ICollection<EnergeticoDivision> EnergeticoDivision { get; set; }
        //public long OldId { get; set; }
    }
}
