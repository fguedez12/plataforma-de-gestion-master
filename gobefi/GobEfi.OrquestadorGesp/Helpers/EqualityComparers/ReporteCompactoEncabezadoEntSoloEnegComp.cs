using OrquestadorGesp.ContextEFNetCore;
using System;
using System.Collections.Generic;

namespace OrquestadorGesp.Helpers.EqualityComparers
{
	public class ReporteCompactoEncabezadoEntSoloEnegComp : IEqualityComparer<ReporteCompactoEncabezadoEnt>
	{
		bool IEqualityComparer<ReporteCompactoEncabezadoEnt>.Equals(ReporteCompactoEncabezadoEnt x, ReporteCompactoEncabezadoEnt y)
		{
			return Convert.ToUInt32(x?.EnergeticoId) == Convert.ToUInt32(y?.EnergeticoId);
		}

		int IEqualityComparer<ReporteCompactoEncabezadoEnt>.GetHashCode(ReporteCompactoEncabezadoEnt obj)
		{
			return Convert.ToInt32(obj?.EnergeticoId);
		}
	}
}
