using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
	// esta clase representa los datos de cada recuadro de año, donde el energetido Id = 0 corresponde al subtotal de año, mes. division
	public class RecuadroSubtotalReporteCompacto : RecuadroTotalReporteCompacto //tiene lo mismo del total salvo que aparte tiene el mes para cada recuadro de año
	{
		public byte Mes { get; set; }
		public new RecuadroSubtotalReporteCompacto Clone()
		{
			return (RecuadroSubtotalReporteCompacto)MemberwiseClone();
		}

		public RecuadroSubtotalReporteCompacto()
		{

		}

		public RecuadroSubtotalReporteCompacto(int anho, byte mes, long divisionId, long energeticoId, float consumo, float costo)
		{
			Mes = mes;
			Anho = anho;
			DivisionId = divisionId;
			EnergeticoId = energeticoId;
			Consumo = consumo;
			Costo = costo;
		}

	}
}
