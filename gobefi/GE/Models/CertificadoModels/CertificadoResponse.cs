using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CertificadoModels
{
    public class CertificadoResponse
    {
        public bool Ok { get; set; }
        public List<CertificadoModel> Certificados { get; set; }
        public string Message { get; set; }
    }
}
