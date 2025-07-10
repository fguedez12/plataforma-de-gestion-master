using OrquestadorGesp.ContextEFNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.Helpers.EqualityComparers
{
	public class ReporteCompactoEncabezadoEntComp : IEqualityComparer<ReporteCompactoEncabezadoEnt>
	{
		bool IEqualityComparer<ReporteCompactoEncabezadoEnt>.Equals(ReporteCompactoEncabezadoEnt x, ReporteCompactoEncabezadoEnt y)
		{
			return GetInt32HashCode(x) == GetInt32HashCode(y);
		}


		int IEqualityComparer<ReporteCompactoEncabezadoEnt>.GetHashCode(ReporteCompactoEncabezadoEnt obj)
		{
			return GetInt32HashCode(obj);
		}

		private static int GetInt32HashCode(ReporteCompactoEncabezadoEnt obj)
		{
			return ((Convert.ToInt32(obj?.EnergeticoId) & 0xff) << 24) | (Convert.ToInt32(obj?.DivisionId) & 0xffffff);
		}
	}
}
