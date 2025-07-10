using System;
using System.Collections.Generic;
using System.Text;

namespace OrquestadorGesp.DTOs
{
  enum EstadoCasillaValor
  {
    NOAP = -2,
    NADA,
    NO ,
    OK,
    SIN_REV,
    CON_OBS
  }
	public class RegistroDataProcesadaControlCarga : BaseDTO
	{
		public RegistroDataProcesadaControlCarga(	int idDivisionCompra
																							, string tipoTransaccion
																							, byte mes, int anho
																							, long idNumeroDeCliente
																							, long idMedidor
																							, string estadoValidacion
																							, byte energeticoId
																							, double cantDiasAcumuladosConsumoMesAnhoDivMed)
		{
			IdDivisionCompra = idDivisionCompra;
			TipoTransaccion = tipoTransaccion;
			Mes = mes;
			Anho = anho;
			IdNumeroDeCliente = idNumeroDeCliente;
			IdMedidor = idMedidor;
			EstadoValidacion = estadoValidacion;
			EnergeticoId = energeticoId;
			EstadoComprasCasilla = (sbyte)EstadoCasillaValor.NO;
      EstadoPorcentajeCasilla = (sbyte)EstadoCasillaValor.NO;
      ValorStrCelda = "N/A";
			CantDiasAcumuladosConsumoMesAnhoDivMed = cantDiasAcumuladosConsumoMesAnhoDivMed;
		}

		public RegistroDataProcesadaControlCarga()
		{

		}
		public int IdDivisionCompra { get; set; }
		//Se ocupará este campo para pintar OK, Noap o NO en la cuadrícula de la hoja Resumen_por_medidor para control carga
		public string TipoTransaccion { get; set; }
		public byte Mes { get; set; }
		public int Anho { get; set; }
		public long IdNumeroDeCliente { get; set; }
		public long IdMedidor { get; set; }
		public string EstadoValidacion { get; set; }
		public byte EnergeticoId { get; set; }

		public double CantDiasAcumuladosConsumoMesAnhoDivMed;
		public sbyte EstadoComprasCasilla { get; set; }
    public sbyte EstadoPorcentajeCasilla { get; set; }
    public string ValorStrCelda { get; set; }
	}
}
