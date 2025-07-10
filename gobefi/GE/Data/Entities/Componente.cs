using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public abstract class Componente : BaseEntity
    {
        private string _nombre;
        private bool _tieneAislacion;
        private double _superficie;
        private long _divisionId;
        private long _tipoMaterialId;
        private long _tipoAislacionId;
        private TipoAislacion _tipoAislacion;
        private TipoMaterial _tipoMaterial;
        private Division _division;

        public string Nombre { get => _nombre; set => _nombre = value; }
        public bool TieneAislacion { get => _tieneAislacion; set => _tieneAislacion = value; }
        public double Superficie { get => _superficie; set => _superficie = value; }

        public long DivisionId { get => _divisionId; set => _divisionId = value; }
        public long TipoMaterialId { get => _tipoMaterialId; set => _tipoMaterialId = value; }
        public long TipoAislacionId { get => _tipoAislacionId; set => _tipoAislacionId = value; }

        public virtual Division Division { get => _division; set => _division = value; }
        public virtual TipoMaterial TipoMaterial { get => _tipoMaterial; set => _tipoMaterial = value; }
        public virtual TipoAislacion TipoAislacion { get => _tipoAislacion; set => _tipoAislacion = value; }
    }
}