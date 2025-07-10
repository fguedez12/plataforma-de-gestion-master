using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
	// Esta clase representa los datos de la derecha de los recuadros de año, para cada recuadro de año,
	// donde Energetico Id=0 -> Total para año de dicho EnergeticoId y DivisionId
	public class RecuadroTotalReporteCompacto : BaseDTO
	{
		public int Anho { get; set; }
		public long DivisionId { get; set; }
		public long EnergeticoId { get; set; } //Energetico Id=0 -> Subtotal para mes año de dicho EnergeticoId y DivisionId
		public float Consumo { get; set; }
		public float Costo { get; set; }
		public RecuadroTotalReporteCompacto Clone()
		{
			return (RecuadroTotalReporteCompacto)MemberwiseClone();
		}

		public RecuadroTotalReporteCompacto()
		{

		}

		public RecuadroTotalReporteCompacto(int anho, long divisionId, long energeticoId, float consumo, float costo)
		{
			Anho = anho;
			DivisionId = divisionId;
			EnergeticoId = energeticoId;
			Consumo = consumo;
			Costo = costo;
		}
	}
}
