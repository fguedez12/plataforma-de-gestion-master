using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.Helpers
{
  public static class DateTimeExtensions
  {
    public static readonly string[] FORMATOS_NOMBRES_MES = new string[] { "M", "MM", "MMM", "MMMM" };
    private static readonly string[] NOMBRE_MES_M = new string[] {"1", "2", "3", "4", "5", "6" , "7", "8", "9" , "10", "12" };
    private static readonly string[] NOMBRE_MES_MM = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "12" };
    private static readonly string[] NOMBRE_MES_MMM = new string[] { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
    private static readonly string[] NOMBRE_MES_MMMM = new string[] { "Enero", "Febrero", "Marzo", "Abril"
                                                                     , "Mayo", "Junio", "Julio", "Agosto"
                                                                      , "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    //private static string[,] NombresMeses = null;
		public enum FormatoMes {
			FORMATO_MES_UN_DIGITO,
			FORMATO_MES_DOS_DIGITOS,
			FORMATO_NOMBRE_MES_CORTO,
			FORMATO_NOMBRE_MES_COMPLETO
		}



		public static DateTime PrimerDiaDelMes(this DateTime current)
		{
			return current.Date.AddDays(1 - current.Day);
		}

		//public static DateTime UltimoDiaDelMes(this DateTime value)
		//{
		//	return value.Date.AddDays(DateTime.DaysInMonth(value.Year, value.Month) - value.Day);
		//}

		public static DateTime UltimoDiaDelMes(this DateTime value)
		{
			return value.PrimerDiaDelMes().AddDays(DateTime.DaysInMonth(value.Year, value.Month) - 1);
		}

		public static string NombreDelMes(int nroMes, FormatoMes formatoMes = FormatoMes.FORMATO_NOMBRE_MES_COMPLETO)
		{
			//if (NombresMeses == null)
			//{
			//	NombresMeses = new string[4,12];
				
			//	for (var enumTipoMes = 0; enumTipoMes < FORMATOS_NOMBRES_MES.Length; enumTipoMes++)
			//	{
			//		var fechaIter = new DateTime(2000, 1, 1);
			//		for (int m = 0; m < 12; m++)
			//		{
			//			NombresMeses[enumTipoMes, m] = fechaIter.AddMonths(m).ToString(FORMATOS_NOMBRES_MES[enumTipoMes]);
			//		}
			//	}
			//}
			if (nroMes < 1) nroMes = 1;
			if (nroMes > 12) nroMes = 12;
      switch (formatoMes)
      {
        case FormatoMes.FORMATO_NOMBRE_MES_COMPLETO:
          return NOMBRE_MES_MMMM[nroMes - 1];
        case FormatoMes.FORMATO_NOMBRE_MES_CORTO:
          return NOMBRE_MES_MMM[nroMes - 1];
        case FormatoMes.FORMATO_MES_DOS_DIGITOS:
          return NOMBRE_MES_MM[nroMes - 1];
        case FormatoMes.FORMATO_MES_UN_DIGITO:
          return NOMBRE_MES_M[nroMes - 1];
        default:
          return NOMBRE_MES_MMMM[nroMes - 1];

      }
		}
	}

}
