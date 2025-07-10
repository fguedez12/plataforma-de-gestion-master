using OrquestadorGesp.ContextEFNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.Helpers.EqualityComparers
{
	public class ReporteControlDeCargaEntSoloDivComp : IEqualityComparer<ReporteControlDeCargaEnt>
	{
		bool IEqualityComparer<ReporteControlDeCargaEnt>.Equals(ReporteControlDeCargaEnt x, ReporteControlDeCargaEnt y)
		{
			return Convert.ToInt64(x?.DivisionId) == Convert.ToInt64(y?.DivisionId);
		}

		int IEqualityComparer<ReporteControlDeCargaEnt>.GetHashCode(ReporteControlDeCargaEnt obj)
		{
			return Convert.ToInt32(obj?.DivisionId);
		}
	}
}
