using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using OrquestadorGesp.AppSettingsJson;
using OrquestadorGesp.Helpers;
using OrquestadorGesp.Reportes;
using Serilog;
using Serilog.Events;
namespace OrquestadorGesp
{
	public class Program
	{
		static int Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
					//.MinimumLevel.Debug()
					.MinimumLevel.Information()
					.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
					//.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
					.Enrich.FromLogContext()
					//.WriteTo.RollingFile(pathFormat: @"logs\orquestador_{Date}.log", fileSizeLimitBytes: 8 * 1024 * 1024, retainedFileCountLimit: 7) //8 MiB MAXIMO LOG DIA, Rango maximo una semana
					.WriteTo.RollingFile(pathFormat: @"logs\Orquestador_FechaISO_{Date}.log"
				, fileSizeLimitBytes: 1024 * 1024 * 2
				, flushToDiskInterval: TimeSpan.FromDays(7)
				, retainedFileCountLimit: null
				) //LOG POR DIA COMPLETO pero maximo archivos de 8 MB
					.CreateLogger();

			try
			{

				//var CURR_CONF = CurrentConfJson.CURRENT_CONF_JSON;
				//Console.WriteLine("SP a ejecutar:" + CURR_CONF.ReporteUnidadesPorServicio.SPUnidadesPorServicio);
				var confJson = GeneralHelper.ObtenerConfJson();
				//ReporteBase.CurrConfGlobal = confJson;
				List<ReporteBase> reportesParaEjecutar = new List<ReporteBase>();
				PropertyInfo[] propertiesInfo = confJson.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
				foreach(PropertyInfo propertyInfo in propertiesInfo)
				{
					ConfReporteBase configReporteBase = null;
          //if (propertyInfo.PropertyType.BaseType == typeof(ConfReporteBase))
          if (ObjectExtensions.IsAncestorOrSameTipe(typeof(ConfReporteBase), propertyInfo.PropertyType.BaseType))
          {
						configReporteBase = (ConfReporteBase)propertyInfo.GetValue(confJson);
            string messageTemplateLoopReportes = string.Format("Tipo Reporte {0} esta {1}", propertyInfo.Name, configReporteBase.Active ? "Activo" : "Inactivo");
            Console.WriteLine(messageTemplateLoopReportes);
            Log.Information(messageTemplateLoopReportes);
            if (configReporteBase.Active)
						{
							string reporteBaseClassName = propertyInfo.Name;
							string namespaceReporteBase = typeof(ReporteBase).Namespace;
							string fullClassNameWithNameSpace = string.Format("{0}.{1}", namespaceReporteBase, reporteBaseClassName);
              Log.Information(messageTemplateLoopReportes);
              ReporteBase reporteBase = (ReporteBase)Activator.CreateInstance(Type.GetType(fullClassNameWithNameSpace));
							reportesParaEjecutar.Add(reporteBase);
              messageTemplateLoopReportes = string.Format("Tipo Reporte {0} activo, inicializado y agregado. Van {1} tipos de reportte", fullClassNameWithNameSpace, reportesParaEjecutar.Count);
              Console.WriteLine(messageTemplateLoopReportes);
              Log.Information(messageTemplateLoopReportes);
            }
					}
				}

				//ReporteUnidadesPorServicio repUni = new ReporteUnidadesPorServicio(); // solo por intanciar se obteniene la data de origen del reporte (SP o comando SQL o CSV)
				//ReporteUnidadesPorServicioConEficEnerg repUniEfic = new ReporteUnidadesPorServicioConEficEnerg(); // solo por intanciar se obteniene la data de origen del reporte (SP o comando SQL o CSV)
				//ReporteExtendido repExt = new ReporteExtendido(); // solo por intanciar se obteniene la data de origen del reporte (SP o comando SQL o CSV)
				//ReporteCompacto repCompact = new ReporteCompacto();
				//Thread hebraReporteUnidadesPorCadaServicio = new Thread(repUni.ObtenerReporte);
				//hebraReporteUnidadesPorCadaServicio.Name = "hebraReporteUnidadesPorCadaServicio";
				//Thread hebraReportePorServicioConEficEnerg = new Thread(repUniEfic.ObtenerReporte);
				//hebraReportePorServicioConEficEnerg.Name = "hebraReportePorServicioConEficEnerg";
				//Thread hebraReporteExtendidoPorCadaServicio = new Thread(repExt.ObtenerReporte);
				//hebraReporteExtendidoPorCadaServicio.Name = "hebraReporteExtendido";
				//Thread hebraReporteCompactoPorCadaServicio = new Thread(repCompact.ObtenerReporte);
				//hebraReporteCompactoPorCadaServicio.Name = "hebraReporteCompacto";

				List<Thread> hebrasConfeccionReportes = new List<Thread>();
				foreach (ReporteBase reporte in reportesParaEjecutar)
				{
					Thread hebraReporte = new Thread(reporte.ObtenerReporte);
					  hebraReporte.Name = reporte.GetType().Name;
					  hebrasConfeccionReportes.Add(hebraReporte);
				}
				//{
				//	hebraReporteCompactoPorCadaServicio
				//	,hebraReporteExtendidoPorCadaServicio
				//	,hebraReporteUnidadesPorCadaServicio
				//	,hebraReportePorServicioConEficEnerg
				//};
				//hebrasConfeccionReportes.Add(hebraReporteExtendido);
				string messageTemplateStartThread = "Hebra {0} iniciada";
				string messageTemplateEndThread = "Hebra {0} finalizada";
				foreach (Thread hebra in hebrasConfeccionReportes)
				{
					hebra.Start();
					var messageThreadStarted = string.Format(messageTemplateStartThread, hebra.Name);
					Console.WriteLine(messageThreadStarted);
					Log.Information(messageThreadStarted);
				}
				foreach ( var hebraIniciada in hebrasConfeccionReportes)
				{
					hebraIniciada.Join();
					var messageThreadFinished = string.Format(messageTemplateEndThread, hebraIniciada.Name);
					Console.WriteLine(messageThreadFinished);
					Log.Information(messageThreadFinished);
				}
				//ReporteUnidadesPorCadaServicio.ObtenerReporte();
				//ReportePorServicioConEficEnerg.ObtenerReporte();
				//Console.WriteLine("Setting from appsettings.json, DBConnName: " + confJson.DBConnName);
				//Console.WriteLine("Setting from appsettings.json, DBConnValue: " + confJson.DBConnValue);
				//Console.WriteLine("Connection string: " + configuration.GetConnectionString("DefaultConnection"));
				string msjOk = "Ejecucion OrquestadorGesp Exitosa";
				Console.WriteLine(msjOk);
				Log.Information(msjOk);
				return 0;
			}
			catch (Exception ex)
			{
				string msjFallo = "Fallo ejecucion. ";
				Console.WriteLine(msjFallo + ex.Message,"\n", ex.StackTrace);
				Log.Fatal(ex, msjFallo);
				return 1;
			}
			finally
			{
				string msjFin = "Termino ejecucion OrquestadorGesp";
				Log.Information(msjFin);
				Console.WriteLine(msjFin);
				Log.CloseAndFlush();
				Console.ReadKey();
			}
		}
	}
}