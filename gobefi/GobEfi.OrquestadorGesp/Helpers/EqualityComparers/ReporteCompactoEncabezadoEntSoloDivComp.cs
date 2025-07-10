using OrquestadorGesp.ContextEFNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.Helpers.EqualityComparers
{
	public class ReporteCompactoEncabezadoEntSoloDivComp : IEqualityComparer<ReporteCompactoEncabezadoEnt>
	{
		bool IEqualityComparer<ReporteCompactoEncabezadoEnt>.Equals(ReporteCompactoEncabezadoEnt x, ReporteCompactoEncabezadoEnt y)
		{
			return Convert.ToInt64(x?.DivisionId) == Convert.ToInt64(y?.DivisionId);
		}

		int IEqualityComparer<ReporteCompactoEncabezadoEnt>.GetHashCode(ReporteCompactoEncabezadoEnt obj)
		{
			return Convert.ToInt32(obj?.DivisionId);
		}
	}
}
