using MYOBCodeChallenge.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayslipServiceTest.Miscellaneous
{
    [TestFixture]
    public class TaxCalculatorTest
    {
        [Test]
        public void TaxCalculatorInputTest()
        {
            var result = TaxCalculator.CalculateIncomeTax(180000);
            Assert.IsNotNull(result);
        }
    }
}
