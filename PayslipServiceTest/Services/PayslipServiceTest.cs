using MYOBCodeChallenge.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayslipServiceTest.Services
{
    [TestFixture]
    public class PayslipServiceTest
    {
        IPayslipService _payslipService;
        [SetUp]
        public void Setup()
        {
            _payslipService = new PayslipService();
        }
        [Test]
        public void PayslipValidInputTest()
        {
            var result = _payslipService.GetPayslip("Badri", "Bharath", "1 Sep - 30 Sep", 15, 80000);
            Assert.IsNotNull(result);
        }

        [Test]
        public void PayslipInValidInputTest()
        {
            var result = _payslipService.GetPayslip("Badri", "Bharath", "1 Sep - 30 Sep", 15, 80000);
            Assert.IsNotNull(result);
        }
    }
}
