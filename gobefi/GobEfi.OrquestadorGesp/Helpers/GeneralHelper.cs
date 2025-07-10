using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Numerics;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using OrquestadorGesp.AppSettingsJson;
using OrquestadorGesp.Helpers.EqualityComparers;

namespace OrquestadorGesp.Helpers
{
	public class GeneralHelper
	{
		public static readonly ReporteCompactoEncabezadoEntComp REPORTE_COMPACTO_ENCABEZADO_ENT_COMPARER = new ReporteCompactoEncabezadoEntComp();
		public static readonly ReporteCompactoEncabezadoEntSoloEnegComp REPORTE_COMPACTO_ENCABEZADO_ENT_SOLO_ENE_COMPARER = new ReporteCompactoEncabezadoEntSoloEnegComp();
		public static readonly ReporteCompactoEncabezadoEntSoloDivComp REPORTE_COMPACTO_ENCABEZADO_ENT_SOLO_DIV_COMPARER = new ReporteCompactoEncabezadoEntSoloDivComp();
    public static readonly ReporteControlDeCargaEntSoloDivComp REPORTE_CONTROL_CARGA_ENT_SOLO_DIV_COMPARER = new ReporteControlDeCargaEntSoloDivComp();
    public static readonly Random RANDOM_NUMBER_GENERATOR = new Random();
    public static string RegexReplace(Regex regex, string strInput, string strReplacement, int startOffset = 0)
		{
			string result = "";
      string firstPartStr = strInput?.Substring(0, startOffset);
      string secondPart = strInput?.Substring(startOffset);
      if (regex != null)
			{
        result = regex.Replace(secondPart, strReplacement);
			}
			return string.Format("{0}{1}", firstPartStr, result);
		}
		public static string RemoveLastChar(string strInput)
		{
			if (string.IsNullOrWhiteSpace(strInput)) return "";
			else return strInput.Remove(strInput.Length - 1);
		}

		public static string ObtenerNombrePropiedad<T>(Expression<Func<T>> propertyExpression)
		{
			return (propertyExpression.Body as MemberExpression).Member.Name;
		}

		public static object ObtenerValorPropiedad(object obj, string nombrePropiedad)
		{
			return obj.GetType().GetProperty(nombrePropiedad).GetValue(obj, new string[0]);
		}

		public static Type ObtenerTipoPropiedad(Type tipoObjetoClase, string nombrePropiedad)
		{
			return tipoObjetoClase.GetProperty(nombrePropiedad)?.PropertyType;
		}

		public static readonly HashSet<Type> TiposEnteros = new HashSet<Type>
		{
			typeof(int),  typeof(long), typeof(short),
			typeof(sbyte),typeof(byte), typeof(ulong),   
			typeof(uint), typeof(BigInteger),
			typeof(int?),  typeof(long?), typeof(short?),
			typeof(sbyte?),typeof(byte?), typeof(ulong?),
			typeof(uint?), typeof(BigInteger?)
		};

		public static readonly HashSet<Type> TiposDecimales = new HashSet<Type>
		{
			typeof(double),  typeof(decimal), typeof(ushort), typeof(float),
			typeof(double?),  typeof(decimal?), typeof(ushort?), typeof(float?)
		};

		public static readonly HashSet<Type> TiposFecha = new HashSet<Type>
		{
			typeof(DateTime), typeof(DateTime?)
		};

		public static readonly HashSet<Type> TiposBooleanos = new HashSet<Type>
		{
			typeof(bool), typeof(bool?)
		};
		public static bool EsNumericoEntero(Type myType)
		{
			var CalzaConAlgunTipo = false;

			if (myType != null)
			{
				CalzaConAlgunTipo = TiposEnteros.Contains(myType);
			}

			return CalzaConAlgunTipo;
		}

		public static bool EsNumericoDecimal(Type myType)
		{
			var CalzaConAlgunTipo = false;

			if (myType != null)
			{
				CalzaConAlgunTipo = TiposDecimales.Contains(myType);
			}

			return CalzaConAlgunTipo;
		}

		public static bool EsBooleano(Type myType)
		{
			var CalzaConAlgunTipo = false;

			if (myType != null)
			{
				CalzaConAlgunTipo = TiposBooleanos.Contains(myType);
			}

			return CalzaConAlgunTipo;
		}

		public static bool EsFechaHora(Type myType)
		{
			var CalzaConAlgunTipo = false;

			if (myType != null)
			{
				CalzaConAlgunTipo = TiposFecha.Contains(myType);
			}

			return CalzaConAlgunTipo;
		}

		public static string DeEnteroHaciaNroRomano(int number)
		{
			return
					new string('I', number)
							.Replace(new string('I', 1000), "M")
							.Replace(new string('I', 900), "CM")
							.Replace(new string('I', 500), "D")
							.Replace(new string('I', 400), "CD")
							.Replace(new string('I', 100), "C")
							.Replace(new string('I', 90), "XC")
							.Replace(new string('I', 50), "L")
							.Replace(new string('I', 40), "XL")
							.Replace(new string('I', 10), "X")
							.Replace(new string('I', 9), "IX")
							.Replace(new string('I', 5), "V")
							.Replace(new string('I', 4), "IV");
		}
		public static ConfJson ObtenerConfJson()
		{
			var builder = new ConfigurationBuilder()
						 .SetBasePath(Directory.GetCurrentDirectory())
						 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

			var configuration = builder.Build();
			var confJson = new ConfJson();
			configuration.GetSection("ConfJson").Bind(confJson);
			return confJson;
		}
	}
	//CurrentConfJson.CurrentConf = CurrConfGlobal;
}
