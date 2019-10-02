using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MYOBCodeChallenge.Controllers;
using MYOBCodeChallenge.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Test]
        public void PayslipValidInputTest()
        {
            var payslipController = new PayslipController(_payslipService);
            var result = payslipController.Generatepayslip("David,Rudd,60050,9%,01 March – 31 March");
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var response = result as OkObjectResult;
            Assert.AreEqual("David Rudd,01 March – 31 March,5004,922,4082,450", response.Value);

        }

        [Test]
        public void PayslipInValidInputTest()
        {
            var payslipController = new PayslipController(_payslipService);
            var result = payslipController.Generatepayslip("David,Rudd,6,,dsf,0050,9%,01 March – 31 March");
            Assert.IsNotNull(result);         
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void GenerateBulkPayslipValidInputTest()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "David,Rudd,60050,9%,01 March – 31 March\nRyan,Chen,120000,10 %,01 March – 31 March";
            var fileName = "validinput.csv";
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms))
            {
                writer.Write(content);
                writer.Flush();
                ms.Position = 0;
                fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
                fileMock.Setup(_ => _.FileName).Returns(fileName);
                fileMock.Setup(_ => _.Length).Returns(ms.Length);
                var file = fileMock.Object;
                var payslipController = new PayslipController(_payslipService);
                var result = payslipController.GenerateBulkPayslip(file);
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<FileStreamResult>(result);
            }
          
        }
        [Test]
        public void GenerateBulkPayslipInValidInputTest()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "David,Rudd,60050,9%,,,,01 March – 31 March\nRyan,Chen,120000,10 %,,,01 March – 31 March";
            var fileName = "validinput.csv";
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms))
            {
                writer.Write(content);
                writer.Flush();
                ms.Position = 0;
                fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
                fileMock.Setup(_ => _.FileName).Returns(fileName);
                fileMock.Setup(_ => _.Length).Returns(ms.Length);
                var file = fileMock.Object;
                var payslipController = new PayslipController(_payslipService);
                var result = payslipController.GenerateBulkPayslip(file);
                Assert.IsNotNull(result);
                Assert.IsInstanceOf<BadRequestObjectResult>(result);
            }
        }


    }
}
