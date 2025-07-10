using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace OrquestadorGesp.Helpers
{
	public class FileSystemHelper : GeneralHelper, IDisposable
	{
		public static readonly string DIR_SEPARATOR = String.Format("{0}", Path.DirectorySeparatorChar);
		public static readonly Regex REGEX_IS_ABSOLUTE_PATH = new Regex(string.Format("^([a-zA-Z]:{0}{0}|{0}{0}{0}{0})", DIR_SEPARATOR));
		public static readonly Regex REGEX_MULTIPLE_CONSECUTIVE_DIR_SEPATORS = new Regex(string.Format($"{DIR_SEPARATOR}{DIR_SEPARATOR}+", DIR_SEPARATOR));

    private FileSystemHelper()
    {

    }
    public static bool RutaEsAbsoluta(string rutaFileSystem)
		{
			return REGEX_IS_ABSOLUTE_PATH.IsMatch(rutaFileSystem, 0);
		}
		public static string LimpiarSeparadoresDirConsecutivos(string ruta, int inicioIndiceCharStr = 0)
		{
			return RegexReplace(REGEX_MULTIPLE_CONSECUTIVE_DIR_SEPATORS, ruta, DIR_SEPARATOR, inicioIndiceCharStr);
		}

    public static FileSystemHelper GetInstance()
    {
      return new FileSystemHelper();
    }

    public virtual bool IsFileLocked(FileInfo file)
		{
			try
			{
				using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
				{
					stream.Close();
				}
			}
			catch (IOException)
			{
				//the file is unavailable because it is:
				//still being written to
				//or being processed by another thread
				//or does not exist (has already been processed)
				return true;
			}

			//file is not locked
			return false;
		}

    #region IDisposable Support
    private bool disposedValue = false; // Para detectar llamadas redundantes

    protected virtual void Dispose(bool disposing)
    {
      if (!disposedValue)
      {
        if (disposing)
        {
          // TODO: elimine el estado administrado (objetos administrados).
        }

        // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
        // TODO: configure los campos grandes en nulos.

        disposedValue = true;
      }
    }

    // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
    // ~FileSystemHelper() {
    //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
    //   Dispose(false);
    // }

    // Este código se agrega para implementar correctamente el patrón descartable.
    public void Dispose()
    {
      // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
      Dispose(true);
      // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
      // GC.SuppressFinalize(this);
    }
    #endregion
  }
}
