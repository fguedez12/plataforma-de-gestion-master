using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CertificadoModels
{
    public class NotasPagingModel
    {
        public int PageIndex { get; set; }
        public int StartPageIndex { get; set; }
        public int StopPageIndex { get; set; }
        public List<NotaModel> Notas { get; set; }
        public List<MinisterioToListModel> Ministerios { get; set; }
        public List<ServicioToListModel> Servicios { get; set; }
    }
}
