using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class TipoDocumento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreE2 { get; set; }
        public int Clasificacion { get; set; }
        public int Orden { get; set;}
        public bool Etapa1 { get; set; }
        public bool Etapa2 { get; set; }
        //1-Comite
        public List<Documento> Documentos { get; set; }
    }
}
