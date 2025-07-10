using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models
{
    public abstract class BaseModel<T>
    {
        [DisplayName("Identificador")]
        [ReadOnly(true)]
        public T Id { get; set; }

        [DisplayName("Fecha de Creación")]
        [ReadOnly(true)]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Fecha de Actualización")]
        [ReadOnly(true)]
        public DateTime UpdatedAt { get; set; }

        [DisplayName("Versión")]
        [ReadOnly(true)]
        public long Version { get; set; }

        [DisplayName("Activo")]
        [ReadOnly(true)]
        public bool Active { get; set; }

        [DisplayName("Usuario de Ultima Modificación")]
        [ReadOnly(true)]
        public string ModifiedBy { get; set; }

        [DisplayName("Usuario de Creación")]
        [ReadOnly(true)]
        public string CreatedBy { get; set; }
    }
}
