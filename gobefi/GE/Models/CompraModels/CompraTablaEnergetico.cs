using GobEfi.Web.Models.MedidorModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CompraModels
{
    public class CompraTablaEnergetico
    {
        //private string _numeroMedidor { get; set; }
        private string _numeroCliente { get; set; }

        public long Id { get; set; }
        public int AnioCompra { get; set; }
        public int AnioFechaInicio { get; set; }
        public int AnioFechaFin { get; set; }
        //public string NumeroMedidor { get{return this._numeroMedidor;} set{this._numeroMedidor = value == null ? "": value;} }
        public string NumeroCliente { get{return this._numeroCliente;} set{this._numeroCliente = value == null ? "": value;} }
        public DateTime InicioLectura { get; set; }
        public DateTime FinLectura { get; set; }
        public DateTime FechaCompra { get; set; }
        public double Consumo { get; set; }
        public string Abrev { get; set; }
        public string Estado { get;set; }
        public string Energetico { get; set; }
        public long EnergeticoId { get; set; }
        public long Costo { get; set; }
        public double Ancho { get; set; }
        public double MarginIzq { get; set; }
        public bool DistintoAnio { get; set; }
        public double  FactorDeConversion { get; set; }
        public long UnidadMedidaId { get; set; }
        public bool SinMedidor { get; set; }
        public string ConsumoEnkWh 
        {
            get
            {
                return (Consumo * FactorDeConversion).ToString("#.##");
            }
        }

        public List<CantidadPorMesClass> CantidadPorMes { get; set; }

        public bool CompraSolapada { get; set; }
        public int CompraSolapadaLv { get; set; }
        public CompraMedidorModels.CompraMedidorForValidate CompraMedidor { get; set; }


        /// <summary>
        /// This method calculates the amount that match with the month and year requested.
        /// If the values of month and year are out of bounds between InicioLectura and FinLectura,
        /// the function returns -1.0
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns>double</returns>
        public double getPercentageMonth(int month, int year)
        {
            int days = DateTime.DaysInMonth(year, month);
            double totalDays = (FinLectura - InicioLectura).TotalDays;
            var lowerDate = DateTime.Parse(year + "-" + month + "-01");
            var upperDate = DateTime.Parse(year + "-" + month + "-" + days);


            //Caso 1 Cuando la compra esta dentro de un mes : el porcentaje es 1 porque abarca la completitud del consumo
            if (lowerDate <= InicioLectura && FinLectura <= upperDate) {

                return 1;
            }


            //caso 2 cuando la compra parte en un mes y termina en otro : se calcula la porcion restando el ultimo dia del mes con
            //el inicio de la medicion
            if (lowerDate <= InicioLectura && InicioLectura <= upperDate && FinLectura > upperDate)
            {
                double dif = (upperDate - InicioLectura).TotalDays;
                return (double)dif / totalDays;
            }


            //caso 3 cuando la compra parte en otro mes y termina en este : se calcula restando al total de los dias de medicion
            // la diferencia en dias de la porcion del mes pasado
            if (lowerDate > InicioLectura && FinLectura >= lowerDate && FinLectura <= upperDate)
            {
                double dif = totalDays - (lowerDate.AddDays(-1) - InicioLectura).TotalDays;
                return (double)dif / totalDays;
            }

            if (lowerDate >InicioLectura && FinLectura > upperDate)
            {
                double dif =days;
                return (double)dif / totalDays;
            }

            return -1.0; // If we reach this point it means that the month required isn't in the range.

            //if (lowerDate <= FinLectura && FinLectura <= upperDate)
            //{
            //    return (double)((FinLectura.Day)/totalDays);
            //}

            //double totalDays = (FinLectura - InicioLectura).TotalDays;
            //int days = DateTime.DaysInMonth(year, month);

            //var lowerDate = DateTime.Parse(year + "-" + month + "-01");
            //var upperDate = DateTime.Parse(year + "-" + month + "-" + days);

            //if (lowerDate <= InicioLectura && InicioLectura <= upperDate)
            //{
            //    int lastDay = (FinLectura <= upperDate) ? FinLectura.Day : days;
            //    return (double)((lastDay - InicioLectura.Day)/totalDays);
            //}

            //if (lowerDate <= FinLectura && FinLectura <= upperDate)
            //{
            //    return (double)((FinLectura.Day)/totalDays);
            //}

            //if (InicioLectura <= lowerDate && upperDate <= FinLectura)
            //{
            //    return (double)(days/totalDays);
            //}

            //return -1.0; // If we reach this point it means that the month required isn't in the range.
        }

        /// <summary>
        /// This method calculates the percentage to fill a cell in a table acording to the amount
        /// of purchase that match with the given month and year. It returns a double between 0 and 1.00.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns>double</returns>
        public double getPercentageFill(int month, int year)
        {
            int days = DateTime.DaysInMonth(year, month);

            var lowerDate = DateTime.Parse(year + "-" + month + "-01");
            var upperDate = DateTime.Parse(year + "-" + month + "-" + days);

            if(lowerDate <= InicioLectura && InicioLectura <= upperDate)
            {
                int lastDay = (FinLectura <= upperDate) ? (int)FinLectura.Day : days;
                return (double)((lastDay - (double)InicioLectura.Day)/days);
            }

            if (lowerDate <= FinLectura && FinLectura <= upperDate)
            {
                return (double)((double)FinLectura.Day/days);
            }

            if (InicioLectura <= lowerDate && upperDate <= FinLectura)
            {
                return 1.0;
            }

            return 0.0; // If we reach this point, it means that the month required isn't in the range.
        }

        /// <summary>
        /// This method returns the amount of Consumo that comes into the given month and year. It returns
        /// a number between 0, if no Consumo match with the given month, and Consumo if all Consumo is
        /// contained in the given month.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public double getMonthAmount(int month, int year)
        {
            var consumo = Consumo;
            var per = getPercentageMonth(month, year);

            var total = consumo * per;
            return total;
        }
    
    }

    public class CantidadPorMesClass {
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string CantidadEnkWh { get; set; }
        public double Llenado { get; set; }


        public bool ContinuaBarra { get; set; }
        public int DiaInicio { get; set; }
        public int DiaFin { get; set; }

    }
}
