using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
  public class ReporteControlDeCargaSPAgrupado
  {
    public ReporteControlDeCargaSPAgrupado()
    {

    }
    public ReporteControlDeCargaSPAgrupado(long divisionId, string division, bool reportaPMG, long regionId, int regionPos, string comuna
      , long nroEmpresaDistId, string empresaDistribuidora, long nroClienteId, string nroCliente
      , long cambioId, string nroMedidorCombinado, long idMedidor, string energetico, long energeticoId)
    {
      DivisionId = divisionId;
      Division = division;
      ReportaPMG = reportaPMG;
      RegionId = regionId;
      RegionPos = regionPos;
      Comuna = comuna;
      NroEmpresaDistId = nroEmpresaDistId;
      EmpresaDistribuidora = empresaDistribuidora;
      NroClienteId = nroClienteId;
      NroCliente = nroCliente;
      CambioId = cambioId;
      NroMedidorCombinado = nroMedidorCombinado;
      IdMedidor = idMedidor;
      Energetico = energetico;
      EnergeticoId = energeticoId;
    }

    public long DivisionId { get; set; }
    public string Division { get; set; }
    public bool ReportaPMG { get; set; }
    public long RegionId { get; set; }
    public int RegionPos { get; set; }
    public string Comuna { get; set; }
    public long NroEmpresaDistId { get; set; }
    public string EmpresaDistribuidora { get; set; }
    public long NroClienteId { get; set; }
    public string NroCliente { get; set; }
    public string Energetico { get; set; }
    public long EnergeticoId { get; set; }
    public long CambioId { get; set; }
    public string NroMedidorCombinado { get; set; }
    public long IdMedidor { get; set; }
  }
}
