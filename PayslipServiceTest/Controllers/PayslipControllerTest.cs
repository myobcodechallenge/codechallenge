using MYOBCodeChallenge.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayslipServiceTest.Controllers
{
    [TestFixture]
    public class PayslipControllerTest
    {
        IPayslipService _payslipService;       
        [SetUp]
        public void Setup()
        {
            _payslipService = new PayslipService();            
        }


    }
}
