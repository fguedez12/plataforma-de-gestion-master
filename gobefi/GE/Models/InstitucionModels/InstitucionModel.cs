using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Models.UsuarioModels;

namespace GobEfi.Web.Models.InstitucionModels
{
    public class InstitucionModel : BaseModel<long>
    {
        //public long Id { get; set; }
        public string Nombre { get; set; }
    }
}
