using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CertificadoModels
{
    public class ServicioToListModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public long MinisterioId { get; set; }
    }
}
