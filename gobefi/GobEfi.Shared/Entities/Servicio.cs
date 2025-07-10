using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Shared.Entities
{
    public class Servicio : BaseEntity
    {
        private string _identificador;
        private long _institucionId;
        private string _nombre;
        private bool _reportaPMG;
        private int _oldId;
        private bool _CompraActiva;
        private Institucion _institucion;
        

        public string Identificador { get => _identificador; set => _identificador = value; }
        public long InstitucionId { get => _institucionId; set => _institucionId = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public bool ReportaPMG { get => _reportaPMG; set => _reportaPMG = value; }
        public int OldId { get => _oldId; set => _oldId = value; }
        public bool CompraActiva { get=> _CompraActiva; set=>_CompraActiva = value; }

        public ICollection<MedidorInteligenteServicio> MedidoresIntelligentesServicios { get; set; }
    }
}
