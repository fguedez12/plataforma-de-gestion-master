using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GobEfi.Web.Models.CompraModels.CompraModel;

namespace GobEfi.WebTest
{
    [TestClass]
    public class CompraTest
    {
        [TestMethod]
        public void TestGetPercentageMonth()
        {
            var compra = new CompraModel
            {
                InicioLectura = DateTime.Parse("2018-01-05"),
                FinLectura = DateTime.Parse("2018-03-15")
            };

            Assert.Equals(compra.getPercentageMonth(3, 2018), 0.0);
        }
    }
}
