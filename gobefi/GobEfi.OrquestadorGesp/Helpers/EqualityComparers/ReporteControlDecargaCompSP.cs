using OrquestadorGesp.ContextEFNetCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.Helpers.EqualityComparers
{
	public class ReporteControlDecargaCompSP : IEqualityComparer<ReporteControlDeCargaEnt>
	{
		bool IEqualityComparer<ReporteControlDeCargaEnt>.Equals(ReporteControlDeCargaEnt x, ReporteControlDeCargaEnt y)
		{
			return GetInt64HashCode(x) == GetInt64HashCode(y);
		}


		int IEqualityComparer<ReporteControlDeCargaEnt>.GetHashCode(ReporteControlDeCargaEnt obj)
		{
			return GetInt32HashCode(obj);
		}

		private static int GetInt32HashCode(ReporteControlDeCargaEnt obj)
		{
			return (((Convert.ToInt32(obj?.EnergeticoId) & 0xff) << 24) | (Convert.ToInt32(obj?.DivisionId) & 0xfffff))
                ^ Convert.ToInt32(obj?.MedidorDivisionId);
		}

        private static long GetInt64HashCode(ReporteControlDeCargaEnt obj)
        {
            return ((Convert.ToInt32(obj?.EnergeticoId) & 0xff) << 56) | ((Convert.ToInt32(obj?.DivisionId) & 0xffffff) << 32)
                | Convert.ToInt32(obj?.MedidorDivisionId);
        }
    }
}
