using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
  public class RegistroCompraProrrateadaAgrupada
  {
    public RegistroCompraProrrateadaAgrupada() { }

    public RegistroCompraProrrateadaAgrupada(RegistroCompraProrrateadaDivEneMesAnho elem, float sumaCosto, float sumaConsumo)
    {
      Elem = elem;
      SumaCosto = sumaCosto;
      SumaConsumo = sumaConsumo;
    }

    public RegistroCompraProrrateadaDivEneMesAnho Elem { get; set; }
    public float SumaCosto { get; set; }
    public float SumaConsumo { get; set; }
  }
}
