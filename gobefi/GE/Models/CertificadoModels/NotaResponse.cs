using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CertificadoModels
{
    public class NotaResponse
    {
        public bool Ok { get; set; }
        public List<NotaModel> Notas { get; set; }
        public string Message { get; set; }
        public NotasPagingModel NotasPorPagina { get; set; }
    }
}
