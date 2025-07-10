using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
	public class RegistroCompraProrrateada : BaseDTO, IEqualityComparer
	{
		public long IdDivisionCompra { get; set; }
		public long EnergeticoId { get; set; }
		public string TipoTransaccion { get; set; }
		public DateTime InicioLectura { get; set; }
		public DateTime FinLectura { get; set; }
		public float Cantidad { get; set; }
		public float Costo { get; set; }
		public long IdNumeroDeCliente { get; set; }

		bool IEqualityComparer.Equals(object x, object y)
		{
			return AmbosSonRegCompraProrrateada(x, y);
		}

		int IEqualityComparer.GetHashCode(object obj)
		{
			if (!(obj is RegistroCompraProrrateada)) return int.MinValue;
			RegistroCompraProrrateada regX = (RegistroCompraProrrateada)obj;
			return ((Convert.ToInt32(regX?.IdDivisionCompra) & 0x3fff) << 14) | ((Convert.ToInt32(regX?.EnergeticoId) & 0xff) << 28) | ((Convert.ToInt32(regX?.FinLectura.Year) & 0x3fff) << 4) | (Convert.ToInt32(regX?.FinLectura.Month & 0xf));
		}

		private static long GetLongHashCode(RegistroCompraProrrateada regComPror)
		{
			long hashLongReg = Convert.ToInt64(regComPror.IdDivisionCompra & 0xffffff) << 40;
			hashLongReg |= Convert.ToInt64(regComPror?.EnergeticoId & 0xff) << 32;
			hashLongReg |= Convert.ToInt64(regComPror?.FinLectura.Year & 0xffff) << 8;
			hashLongReg |= Convert.ToInt64(regComPror?.FinLectura.Month & 0xff);
			return hashLongReg;
		}

		private static bool AmbosSonRegCompraProrrateada(object x, object y)
		{
			long hashLongX = 0;
			long hashLongY = 0;
			int igualdadTipos = Convert.ToByte(x is RegistroCompraProrrateada) & Convert.ToByte(y is RegistroCompraProrrateada);
			if (igualdadTipos == 0) return false;
			RegistroCompraProrrateada regX = (RegistroCompraProrrateada)x;
			RegistroCompraProrrateada regY = (RegistroCompraProrrateada)x;
			hashLongX = GetLongHashCode(regX);
			hashLongY = GetLongHashCode(regY);
			return hashLongX == hashLongY;
		}
	}
}
