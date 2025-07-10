using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OrquestadorGesp.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace OrquestadorGesp.Helpers
{
	public class ExcelHelper : GeneralHelper
	{
		public static readonly DateTime EXCEL_DATE_REF = new DateTime(1900, 1, 1);

		public enum DesplazamientoContenidoHoja
		{
			ATRAS = -1,
			NADA,
			ADELANTE
		}

		public static int MoverUltimaFilaHastaIndice(ISheet hoja, int indiceDestino)
		{
			int cantFilasEliminadas = 0;
			int nroUltimaFila = hoja.LastRowNum;
			while (indiceDestino < nroUltimaFila)
			{
				hoja.ShiftRows(nroUltimaFila, nroUltimaFila, -1);
				nroUltimaFila = hoja.LastRowNum;
				cantFilasEliminadas++;
			}
			return cantFilasEliminadas;
		}

		public const short FORMATO_NPOI_DOS_DECIMALES = 2;
		public const short FORMATO_NPOI_SIN_DECIMALES = 1;
		public const short FORMATO_NPOI_GENERAL = 0;
		public const short FORMATO_NPOI_FECHA_DD_MM_YYYY = 12;
		public static IRow CopiarReemplazarFila(ISheet hojaExcelContexto, int nroFilaOrigen, int nroFilaDestino)
		{
			// Get the source / new row
			IRow sourceRow = hojaExcelContexto.GetRow(nroFilaOrigen);
			IRow newRow = hojaExcelContexto.GetRow(nroFilaDestino);

			// If the row exist in destination, push down all rows by 1 else create a new row
			if (newRow == null)
			{
				newRow = hojaExcelContexto.CreateRow(nroFilaDestino);
			}

			// Loop through source columns to add to new row
			for (int i = 0; i < sourceRow.LastCellNum; i++)
			{
				// Grab a copy of the old/new cell
				ICell oldCell = sourceRow.GetCell(i);
				ICell newCell = newRow.CreateCell(i);

				// If the old cell is null jump to next cell
				if (oldCell == null)
				{
					newCell = null;
					continue;
				}

				// Copy style from old cell and apply to new cell
				ICellStyle newCellStyle = oldCell.CellStyle;
				newCellStyle.CloneStyleFrom(newCellStyle); ;
				newCell.CellStyle = newCellStyle;

				// Set the cell data type
				newCell.SetCellType(oldCell.CellType);

				// Set the cell data value
				switch (oldCell.CellType)
				{
					case CellType.Blank:
						newCell.SetCellValue(oldCell.StringCellValue);
						break;
					case CellType.Formula:
						newCell.CellFormula = oldCell.CellFormula;
						//Si tenemos que modificar la formulario lo podemos hacer como string
						//oldCell.getCellFormula().replace("A"+sourceRowNum, "A"+destinationRowNum)
						break;
					case CellType.Numeric:
						newCell.SetCellValue(oldCell.NumericCellValue);
						break;
					case CellType.String:
						newCell.SetCellValue(oldCell.RichStringCellValue);
						break;
				}
			}
			//..
			//..
			return newRow;
		}

		/**
		 * No funciona, copia la colummna de recuadro de total (columna V) a la primera columna donde empiezan
		 * los recuadros (columna J) pero NO SE DESPLAZAN LAS COLUMNAS. ya se había implementado
		 * algo pero con filas. Con columnas es mucho más complejo
		 */
		public static void CopiarRangoColumnas(ISheet hoja, int colOrigen, int colDestino, int cantCols, int nroFilaInicial = 0)
		{
			//int nrofilaInicial = 5;
			int nrofilaFinal = hoja.PhysicalNumberOfRows - 1;
			int colOrigenIter = 0;
			IRow filaIter = null;
			//int cantMeses = 12;
			//int largoSaltoRecuadro = cantMeses + 1;
      if (colOrigen == colDestino) return;
			//ICell celdaNueva;
			for (int i = nroFilaInicial; i <= nrofilaFinal; i++)
			{
				filaIter = hoja.GetRow(i);
				for (int j = colDestino; j < colDestino + cantCols; j++)
				{
					colOrigenIter = colOrigen + j - colDestino;
					int columnWidthOrigen = hoja.GetColumnWidth(colOrigenIter);
					filaIter.CopyCell(colOrigenIter, j);
					hoja.SetColumnWidth(j, columnWidthOrigen);
				}
			}
		}

		public static DateTime ConvertDateTime(double excelDate)
		{
			if (excelDate < 1)
			{
				excelDate = 1;
				//throw new ArgumentException("Excel dates cannot be smaller than 0.");
			}

			var dateOfReference = EXCEL_DATE_REF;

			if (excelDate > 60d)
				excelDate = excelDate - 2;
			else
				excelDate = excelDate - 1;
			return dateOfReference.AddDays(excelDate);
		}


    public static List<T> ObtenerInfoDesdeInsumoXlsx<T>(string rutaAbsolutaArchivoXlsx
      , int startCol
      , int startRow
      , Dictionary<string, string> nombresCamposExcel
      , int indiceHoja = 0
      , int endCol = -1
      , int endRow = -1) where T : new()
      //(this ExcelWorksheet worksheet, Dictionary<string, string> map = null) where T : new()
    {
      XSSFWorkbook workbook = null;
      XSSFSheet worksheet = null;
      //DateTime Conversion
      //using (ExcelWorksheet worksheet = ObtenerHojaDesdeLibro(rutaAbsolutaArchivoXlsx, indiceHoja))
      using (FileStream fsReadOnly = new FileStream(rutaAbsolutaArchivoXlsx, FileMode.Open, FileAccess.Read))
      {
        workbook = new XSSFWorkbook(fsReadOnly);
        worksheet = (XSSFSheet)workbook.GetSheetAt(indiceHoja);
        fsReadOnly.Close();
      }

      var props = typeof(T).GetProperties()
      //.Select(prop =>
      //{
      //	var displayAttribute = (DisplayAttribute)prop.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
      //	return new
      //	{
      //		prop.Name,
      //		DisplayName = displayAttribute == null ? prop.Name : displayAttribute.Name,
      //		Order = displayAttribute == null || !displayAttribute.GetOrder().HasValue ? 999 : displayAttribute.Order,
      //		PropertyInfo = prop,
      //		prop.PropertyType,
      //		HasDisplayName = displayAttribute != null
      //	};
      //})
      .Where(prop => !string.IsNullOrWhiteSpace(prop.Name))
      .ToDictionary(p => p.Name);

      var retList = new List<T>();
      //var columns = new List<ExcelMap>();

      endRow = endRow < 0 ? worksheet.LastRowNum : endRow;

      endCol = endCol < 0 ? startCol + worksheet.GetRow(0).LastCellNum : endCol;
      //var endRow = end.Row;

      // Assume first row has column names in the SAME ORDER as nombresCamposExcel
      //for (int i = 0; i < nombresCamposExcel.Length; i++)
      //{
      //	columns.Add(new ExcelMap()
      //	{
      //		Name = nombresCamposExcel[i],
      //		MappedTo = nombresCamposExcel[i],
      //		Index = startCol + i + 1
      //	});
      //}
      string startColStr = CellReference.ConvertNumToColString(startCol);
      string endColStr = CellReference.ConvertNumToColString(endCol);
      string colIndexStr = string.Empty;
      //string messageInitreadExcelRepExt = string.Format("Por leer {0} filas (desde fila {1} y columnas desde {2} hacia {3} de Excel \"{4}\""
      //	, endRow - startRow + 1, startRow + 1, startColStr, endColStr, rutaAbsolutaArchivoXlsx);
      //Console.WriteLine(messageInitreadExcelRepExt);
      //Log.Information(messageInitreadExcelRepExt);
      // Now iterate over all the rows
      for (int rowIndex = startRow; rowIndex <= endRow; rowIndex++)
      {
        var item = new T();
        IRow row = worksheet.GetRow(rowIndex);
        for (int colIndex = startCol; colIndex <= endCol; colIndex++)
        {
          //string colIndexStr = CellReference.ConvertNumToColString(colIndex);
          //var columnIndexStr = CellReference.ConvertNumToColString(column.Index - 1);
          //columnIndexStr
          //string mensajeIteracionLecturaExcelRepExt = string.Format("Reading Cell [{0}{1}] mapped to field \"{2}\"",
          //	colIndexStr, rowIndex + 1, nombresCamposExcel[colIndex - startCol]);
          //var value = worksheet.Cells[rowIndex, column.Index].Value;
          colIndexStr = CellReference.ConvertNumToColString(colIndex);
          if (!nombresCamposExcel.ContainsKey(colIndexStr)) continue;
          if (row == null) continue;
          ICell cell = row.GetCell(colIndex);
          if (cell == null) continue;
          //var value = worksheet.Cells[rowIndex + 1, colIndex + 1].Value;
          //var valueStr = value == null ? string.Empty : value.ToString().Trim();
          object value = null;
          var valueStr = string.Empty;
          switch (cell.CellType)
          {
            case CellType.Numeric:
              value = cell.NumericCellValue;
              valueStr = Convert.ToString(value);
              break;
            case CellType.Boolean:
              value = cell.BooleanCellValue;
              valueStr = Convert.ToString(value);
              break;
            case CellType.String:
              value = cell.StringCellValue;
              valueStr = cell.StringCellValue;
              break;
            default:
              CellReference cellRef = new CellReference(cell);
              value = cellRef.CellRefParts.GetValue(0);
              valueStr = Convert.ToString(value);
              break;
          }
          //Console.WriteLine("{0}\nValor=[{1}], ValorStr=[{2}]", mensajeIteracionLecturaExcelRepExt, value, valueStr);
          //Log.Information("{0}\nValor=[{1}], ValorStr=[{2}]", mensajeIteracionLecturaExcelRepExt, value, valueStr);


          var prop = props[nombresCamposExcel[colIndexStr]];

          // Excel stores all numbers as doubles, but we're relying on the object's property types
          if (prop == null) continue;
          var propertyType = prop.PropertyType;
          object parsedValue = null;

          if (propertyType == typeof(byte?) || propertyType == typeof(byte))
          {
            byte val;
            if (!byte.TryParse(valueStr, out val))
            {
              val = default(byte);
            }

            parsedValue = val;
          }
          else if (propertyType == typeof(sbyte?) || propertyType == typeof(sbyte))
          {
            sbyte val;
            if (!sbyte.TryParse(valueStr, out val))
            {
              val = default(sbyte);
            }

            parsedValue = val;
          }
          else if (propertyType == typeof(int?) || propertyType == typeof(int))
          {
            int val;
            if (!int.TryParse(valueStr, out val))
            {
              val = default(int);
            }

            parsedValue = val;
          }
          else if (propertyType == typeof(short?) || propertyType == typeof(short))
          {
            short val;
            if (!short.TryParse(valueStr, out val))
              val = default(short);
            parsedValue = val;
          }
          else if (propertyType == typeof(long?) || propertyType == typeof(long))
          {
            long val;
            if (!long.TryParse(valueStr, out val))
              val = default(long);
            parsedValue = val;
          }
          else if (propertyType == typeof(decimal?) || propertyType == typeof(decimal))
          {
            decimal val;
            if (!decimal.TryParse(valueStr, out val))
              val = default(decimal);
            parsedValue = val;
          }
          else if (propertyType == typeof(double?) || propertyType == typeof(double))
          {
            double val;
            if (!double.TryParse(valueStr, out val))
              val = default(double);
            parsedValue = val;
          }
          else if (propertyType == typeof(DateTime?) || propertyType == typeof(DateTime))
          {
            parsedValue = ConvertDateTime((double)value);
          }
          else if (propertyType.IsEnum)
          {
            try
            {
              parsedValue = Enum.ToObject(propertyType, int.Parse(valueStr));
            }
            catch
            {
              parsedValue = Enum.ToObject(propertyType, 0);
            }
          }
          else if (propertyType == typeof(string))
          {
            parsedValue = valueStr;
          }
          else
          {
            try
            {
              parsedValue = Convert.ChangeType(value, propertyType);
            }
            catch
            {
              parsedValue = valueStr;
            }
          }

          try
          {
            //prop.PropertyInfo.SetValue(item, parsedValue);
            prop.SetValue(item, parsedValue);
          }
          catch (Exception ex)
          {
            string mensajeExcepcionLecturaCeldaRepExt = string.Format("Excepcion al leer celda {0}{1}. {2}:{3}. Traza:\n{4}"
              , colIndexStr, rowIndex + 1, ex.GetType().Name, ex.Message, ex.StackTrace);
            Console.WriteLine(mensajeExcepcionLecturaCeldaRepExt);
            Log.Warning(mensajeExcepcionLecturaCeldaRepExt);
            // Indicate parsing error on row?
          }
        }

        retList.Add(item);
      }
      worksheet = null;
      workbook.Close();
      return retList;
    }

  }

  //   public static List<T> ObtenerInfoDesdeInsumoXlsx<T>(string rutaAbsolutaArchivoXlsx
  //	, int startCol
  //	, int startRow
  //	, Dictionary<string, string> nombresCamposExcel
  //	, int indiceHoja = 0
  //    , int endCol = -1
  //    , int endRow = -1) where T : new()
  //	//(this ExcelWorksheet worksheet, Dictionary<string, string> map = null) where T : new()
  //{
  //	XSSFWorkbook workbook = null;
  //	XSSFSheet worksheet = null;
  //	//DateTime Conversion
  //	//using (ExcelWorksheet worksheet = ObtenerHojaDesdeLibro(rutaAbsolutaArchivoXlsx, indiceHoja))
  // }
  //  public ManejarDataInsumo()
  //public class ExcelMap
  //{
  //	public string Name { get; set; }
  //	public string MappedTo { get; set; }
  //	public int Index { get; set; }
  //}
}
