using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
  public class RegistroCompraProrrateadaDivEneMesAnho
  {
    public RegistroCompraProrrateadaDivEneMesAnho() { }

    public RegistroCompraProrrateadaDivEneMesAnho(long idDivisionCompra, long energeticoId, int year, int month)
    {
      IdDivisionCompra = idDivisionCompra;
      EnergeticoId = energeticoId;
      Year = year;
      Month = month;
    }

    public long IdDivisionCompra { get; set; }
    public long EnergeticoId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
  }
}
