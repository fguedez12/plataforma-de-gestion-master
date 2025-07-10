using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
  public class RegistroCompraProrrateadaDivMesAnhoCostoConsumo
  {
    public RegistroCompraProrrateadaDivMesAnho Elem { get; set; }
    public float SumaCostoDivMesAnho { get; set; }
    public float SumaConsumoDivMesAnho { get; set; }
  }
}
