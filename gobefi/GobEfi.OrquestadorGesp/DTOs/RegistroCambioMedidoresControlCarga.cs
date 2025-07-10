using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
  public class RegistroCambioMedidoresControlCarga
  {
    public RegistroCambioMedidoresControlCarga() {}

    public RegistroCambioMedidoresControlCarga(long cambioId, long medidorId, long medidorNuevoId, long medidorAntiguoId)
    {
      CambioId = cambioId;
      MedidorId = medidorId;
      MedidorNuevoId = medidorNuevoId;
      MedidorAntiguoId = medidorAntiguoId;
    }

    public long CambioId { get; set; }
    public long MedidorId { get; set; }
    public long MedidorNuevoId { get; set; }
    public long MedidorAntiguoId { get; set; }
  }
}
