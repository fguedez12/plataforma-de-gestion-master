using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EstadoValidacion
    {
        [Key]
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}
