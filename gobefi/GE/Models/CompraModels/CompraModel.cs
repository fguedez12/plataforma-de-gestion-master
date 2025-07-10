using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.MedidorModels;
using GobEfi.Web.Models.NumeroClienteModels;
using GobEfi.Web.Models.UnidadMedidaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.CompraModels
{
    public class CompraModel : BaseModel<long>
    {
        public long EnergeticoId { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime InicioLectura { get; set; }
        public DateTime FinLectura { get; set; }
        public double Cantidad { get; set; }
        public double Costo { get; set; }
        public string Factura { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        public string ObservacionRevision { get; set; }
        public long NumClientesId { get; set; }
        public long MedidoresId { get; set; }

        public UnidadMedidaModel UnidadMedida { get; set; }

        public IEnumerable<EnergeticoModel> Energetico { get; set; }
        public IEnumerable<NumeroClienteModel> NumClientes { get; set; }
        public IEnumerable<MedidorModel> Medidores { get; set; }




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
            double totalDays = (FinLectura - InicioLectura).TotalDays;
            int days = DateTime.DaysInMonth(year, month);

            var lowerDate = DateTime.Parse(year + "-" + month + "-01");
            var upperDate = DateTime.Parse(year + "-" + month + "-" + days);

            if (lowerDate <= InicioLectura && InicioLectura <= upperDate)
            {
                int lastDay = (FinLectura <= upperDate) ? FinLectura.Day : days;
                return (double)((lastDay - InicioLectura.Day)/totalDays);
            }

            if (lowerDate <= FinLectura && FinLectura <= upperDate)
            {
                return (double)((FinLectura.Day)/totalDays);
            }

            if (InicioLectura <= lowerDate && upperDate <= FinLectura)
            {
                return (double)(days/totalDays);
            }
            
            return -1.0; // If we reach this point it means that the month required isn't in the range.
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
        /// This method returns the amount of Cantidad that comes into the given month and year. It returns
        /// a number between 0, if no Cantidad match with the given month, and Cantidad if all Cantidad is
        /// contained in the given month.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public double getMonthAmount(int month, int year)
        {
            return Cantidad*getPercentageMonth(month, year);
        }
    }
}
