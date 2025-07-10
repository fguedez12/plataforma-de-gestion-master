using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.InstitucionModels
{
    public class InstitucionIndexModel : BasePagedModel<InstitucionListModel>
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
    }
}
