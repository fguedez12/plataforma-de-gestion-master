using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
  public class RegistroReporteCompactoListIzq
  {
    public long IdDivision { get; set; }
    public long EnergeticoId { get; set; }
    public string Division { get; set; }
    public string Energetico { get; set; }
    public bool MedidorExclusivo { get; set; }
    public bool MedidorMixto { get; set; }
    public float? Superficie { get; set; }
    public string ClasificacionEdificio { get; set; }
    public string TipoEdificio { get; set; }
    public string CalcularTipoEdificio { get; set; }
    public string RegionNom { get; set; }
    public int RegionPos { get; set; }
    public string Comuna { get; set; }
  }
}
