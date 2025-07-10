using GobEfi.Web.Models.CompraModels;
using System;
using Xunit;
namespace Test
{
    public class CompraModelTest
    {
        [Fact]
        public void TestCompraMonth()
        {
            var compraAcross3Months = new CompraModel
            {
                Cantidad = 100.0,
                InicioLectura = DateTime.Parse("2018-01-05"),
                FinLectura = DateTime.Parse("2018-03-15")
            };

            var compraLessThanMonth = new CompraModel
            {
                Cantidad = 100.0,
                InicioLectura = DateTime.Parse("2018-01-05"),
                FinLectura = DateTime.Parse("2018-01-28")
            };

            // Check for data in the upper boundary from range
            Assert.Equal(0.217391, compraAcross3Months.getPercentageMonth(3, 2018), 6);
            Assert.Equal(0.483871, compraAcross3Months.getPercentageFill(3, 2018), 6);
            Assert.Equal(21.739130, compraAcross3Months.getMonthAmount(3, 2018), 6);

            // Check for data in the lower boundary from range
            Assert.Equal(0.376812, compraAcross3Months.getPercentageMonth(1, 2018), 6);
            Assert.Equal(0.838710, compraAcross3Months.getPercentageFill(1, 2018), 6);
            Assert.Equal(37.681159, compraAcross3Months.getMonthAmount(1, 2018), 6);

            // Check for data in the midle
            Assert.Equal(0.405797, compraAcross3Months.getPercentageMonth(2, 2018), 6);
            Assert.Equal(1.000000, compraAcross3Months.getPercentageFill(2, 2018), 6);
            Assert.Equal(40.57971, compraAcross3Months.getMonthAmount(2, 2018), 6);

            // Check for data out of boundaries from range
            Assert.Equal(-1.0, compraAcross3Months.getPercentageMonth(4, 2018), 6);
            Assert.Equal(0.0, compraAcross3Months.getPercentageFill(4, 2018), 6);
            Assert.Equal(-(compraAcross3Months.Cantidad), compraAcross3Months.getMonthAmount(4, 2018), 6);
            Assert.Equal(-1.0, compraAcross3Months.getPercentageMonth(12, 2017), 6);
            Assert.Equal(0.0, compraAcross3Months.getPercentageFill(12, 2017), 6);
            Assert.Equal(-(compraAcross3Months.Cantidad), compraAcross3Months.getMonthAmount(12, 2017), 6);

            // Check for purchases with InicioLectura and FinLectura less than a month
            Assert.Equal(1.0, compraLessThanMonth.getPercentageMonth(1, 2018), 6);
            Assert.Equal(0.741935, compraLessThanMonth.getPercentageFill(1, 2018), 6);
            Assert.Equal(100.0, compraLessThanMonth.getMonthAmount(1, 2018), 6);
        }
    }
}
