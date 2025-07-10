using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DisenioPasivoModels
{
    public class ArchivoDpModel
    {
        public long Id { get; set; }
        public string NombreArchivo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long Peso { get; set; }
        public string Seccion { get; set; }
        public string FileUrl { get; set; }
        public long DivisionId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
