using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GobEfi.Web.Data.Entities
{
    public class EndPoint
    {
        public long Id { get; set; }
        [Required]
        public string ControllerName { get; set; }
        [Required]
        public string ActionName { get; set; }
        public ICollection<PermisosBackEnd> PermisosBackEnd { get; set; }
    }
}
