using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.GZip;
using GobEfi.Business.Security;
using GobEfi.Business.Registro;
using System.Net.Mail;
using System.Net;

namespace GobEfi.ReportesDiarios
{
    public class Ejecucion
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["GE2.0"].ConnectionString;
        private readonly string directorioTemp = ConfigurationManager.AppSettings["RutaTemp"];
        private readonly string rutaDestino = ConfigurationManager.AppSettings["RutaDestino"];


        #region Datos impersonalizacion
        public string Usuario
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["usuario"].ToString();
                }
                catch
                {
                    throw new Exception("Debe especificar Usuario en config.");
                }
            }
        }

        public string Contraseña
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["clave"].ToString();
                }
                catch
                {
                    throw new Exception("Debe especificar Contraseña en config.");
                }
            }
        }

        public string Dominio
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["dominio"].ToString();
                }
                catch
                {
                    throw new Exception("Debe especificar Dominio en config.");
                }
            }
        }
        #endregion


        private readonly LogEvent _log = new LogEvent();

        public Ejecucion()
        {
        }

        internal void Iniciar()
        {
            //Email("Ejecucion del proceso Finalizada");
            string nombreCarpetaReportes = $"reportes_{ DateTime.Now.ToString("yyyy-MM-dd")}";
            DirectoryInfo carpetaTemporal = Directory.CreateDirectory($"{directorioTemp}{Path.DirectorySeparatorChar}{nombreCarpetaReportes}");

            _log.Registrar(this.GetType().ToString(), $"Comenzando ejecucion para {nombreCarpetaReportes}.", LogLevel.Information);

            try
            {
                // Indicar la cantidad de reportes
                List<Reporte> reportes = ObtenerReportes();

                // Conectar a la base de datos y obtener reporte 
                ObtenerDatosDeSp(reportes);

                // generar csv temporal del reporte
                GenerarCsv(reportes, carpetaTemporal);

                // comprimir reportes
                string archivoTemporal = $"{directorioTemp}{Path.DirectorySeparatorChar}{nombreCarpetaReportes}.tar.gz";
                CreateTar(archivoTemporal, carpetaTemporal.FullName);

                _log.Registrar(this.GetType().ToString(), $"Compresion de archivo {archivoTemporal} listo.", LogLevel.Information);

                // Enviar todos los excel al repositorio
                using (WindowsLogin wl = new WindowsLogin(Usuario, Dominio, Contraseña))
                {
                    System.Security.Principal.WindowsIdentity.RunImpersonated(wl.Identity.AccessToken, () =>
                    {
                        File.Copy(archivoTemporal, Path.Combine(rutaDestino, $"{nombreCarpetaReportes}.tar.gz"), true);
                    });
                };

                _log.Registrar(this.GetType().ToString(), $"Copiando archivo a directorio {rutaDestino}.", LogLevel.Information);

                // Eliminar los excel temporales generados
                EliminarArchivos(directorioTemp, true);
                _log.Registrar(this.GetType().ToString(), $"Archivos temporales eliminados {directorioTemp}.", LogLevel.Information);
            }
            catch (Exception ex)
            {

                _log.Registrar(this.GetType().ToString(), ex, LogLevel.Error);
                Email("Ocurrio un error en el proceso");
            }
            finally
            {
                Email("<p>Ejecucion del proceso Finalizada</p>");
                _log.Registrar(this.GetType().ToString(), $"Ejecucion finalizada para {nombreCarpetaReportes}.", LogLevel.Information);
            }

            
        }

        private static void Email(string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("no-reply@minenergia.cl","[MINENERGIA] Gestiona Energia");
                message.To.Add(new MailAddress("hlopez@minenergia.cl"));
                message.To.Add(new MailAddress("hsepulveda@minenergia.cl"));
                message.To.Add(new MailAddress("lgarcia@minenergia.cl"));
                message.Subject = "Ejecucion de proceso de reportes";
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 26;
                smtp.Host = "10.0.0.245";
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = null;
                smtp.Send(message);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void EliminarArchivos(string ruta, bool recursivo, bool esSubCarpeta = false)
        {
            if (Directory.Exists(ruta))
            {
                // Recursively add sub-folders
                if (recursivo)
                {
                    string[] subCarpetas = Directory.GetDirectories(ruta);
                    foreach (string subCarpeta in subCarpetas)
                        EliminarArchivos(subCarpeta, recursivo, true);
                }

                string[] archivos = Directory.GetFiles(ruta);

                foreach (string archivo in archivos)
                {
                    File.Delete(archivo);
                }


                if (esSubCarpeta)
                {
                    Directory.Delete(ruta);
                }

            }
        }

        /// <summary>
        /// Creates a GZipped Tar file from a source directory
        /// </summary>
        /// <param name="outputTarFilename">Output .tar.gz file</param>
        /// <param name="sourceDirectory">Input directory containing files to be added to GZipped tar archive</param>
        private void CreateTar(string outputTarFilename, string sourceDirectory)
        {
            using (FileStream fs = new FileStream(outputTarFilename, FileMode.Create, FileAccess.Write, FileShare.None))
            using (Stream gzipStream = new GZipOutputStream(fs))
            using (TarArchive tarArchive = TarArchive.CreateOutputTarArchive(gzipStream))
            {
                AddDirectoryFilesToTar(tarArchive, sourceDirectory, true);
            };
        }

        /// <summary>
        /// Recursively adds folders and files to archive
        /// </summary>
        /// <param name="tarArchive"></param>
        /// <param name="sourceDirectory"></param>
        /// <param name="recurse"></param>
        private void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory, bool recurse)
        {
            // Recursively add sub-folders
            if (recurse)
            {
                string[] directories = Directory.GetDirectories(sourceDirectory);
                foreach (string directory in directories)
                    AddDirectoryFilesToTar(tarArchive, directory, recurse);
            }

            // Add files
            string[] filenames = Directory.GetFiles(sourceDirectory);
            foreach (string filename in filenames)
            {
                TarEntry tarEntry = TarEntry.CreateEntryFromFile(filename);
                tarArchive.WriteEntry(tarEntry, true);
            }
        }

        private void GenerarCsv(List<Reporte> reportes, DirectoryInfo directorio)
        {
            foreach (Reporte reporte in reportes)
            {
                // StringBuilder sb = new StringBuilder();
                try
                {
                    IEnumerable<string> columnNames = reporte.Datos.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);


                    var archivo = new FileStream($"{directorio.FullName}{Path.DirectorySeparatorChar}{reporte.Nombre}{reporte.Ext}", FileMode.OpenOrCreate);

                    using (var sw = new StreamWriter(archivo, Encoding.UTF8))
                    {
                        sw.WriteLine(string.Join($"\t", columnNames));
                        sw.Flush();

                        foreach (DataRow row in reporte.Datos.Rows)
                        {
                            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Trim().Replace("\t", " ").Replace("\n", " "));

                            var textLine = string.Join("\t", string.Join("\t", fields).Replace(Environment.NewLine, ""));

                            sw.WriteLine(textLine);
                            sw.Flush();
                        }
                    };


                    _log.Registrar(this.GetType().ToString(), $"Archivo {archivo.Name} generado.", LogLevel.Information);
                }
                catch (Exception)
                {
                    // Do nothing
                }
            }
        }

        private void ObtenerDatosDeSp(List<Reporte> reportes)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                foreach (Reporte reporte in reportes)
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable dt = new DataTable();

                    try
                    {
                        cmd = new SqlCommand(reporte.ProcedimientoAlmacenado, conn)
                        {
                            CommandType = CommandType.StoredProcedure,
                            CommandTimeout = 0
                        };

                        da.SelectCommand = cmd;
                        _log.Registrar(this.GetType().ToString(), $"Obteniendo los datos del reporte {reporte.Nombre}", LogLevel.Information);


                        da.Fill(dt);


                        reporte.Datos = dt;
                    }
                    catch (Exception ex)
                    {
                        _log.Registrar(this.GetType().ToString(), ex, LogLevel.Error);
                    }
                    finally
                    {
                        cmd.Dispose();
                    }
                }

            };

        }

        private List<Reporte> ObtenerReportes()
        {
            
            List<Reporte> listaReportes = new List<Reporte>();

            using (var conn = new SqlConnection(connectionString))
            {
                var query = @"SELECT a.Nombre, a.ProcedimientoAlmacenado, b.Extension FROM reportes a
                            INNER JOIN gestiona_energia..TipoArchivos b ON a.TipoArchivoId = b.Id
                            WHERE SeGeneraAutomatico = 1 and Activo = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();

                try
                {
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    _log.Registrar(this.GetType().ToString(), ex, LogLevel.Error);
                }

                foreach (DataRow fila in dt.Rows)
                {
                    _log.Registrar(this.GetType().ToString(), $"Guardando configuracion del reporte: {fila["Nombre"].ToString()}", LogLevel.Information);

                    var reporte = new Reporte();

                    reporte.Nombre = fila["Nombre"].ToString();
                    reporte.ProcedimientoAlmacenado = fila["ProcedimientoAlmacenado"].ToString();
                    reporte.Ext = fila["Extension"].ToString();

                    listaReportes.Add(reporte);
                }
            };

            return listaReportes;
        }
    }

    public class Reporte
    {
        public string Nombre { get; set; }
        public string Ext { get; set; }
        public string ProcedimientoAlmacenado { get; set; }
        public DataTable Datos { get; set; }
    }
}
