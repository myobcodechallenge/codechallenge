﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MYOB.Models;
using MYOBCodeChallenge.Services;
using MYOBCodeChallenge.Utilities;

namespace MYOBCodeChallenge.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PayslipController : ControllerBase
    {
        IPayslipService _payslipService;

        public PayslipController(IPayslipService payslipService)
        {
            _payslipService = payslipService;
        }
        [HttpPost]
        public IActionResult Generatepayslip([FromBody]string input)
        {

            var employee = ValidateInputGetEmployye(input);
            if (employee != null)
            {
                var paydetails = _payslipService.GetPayslip(employee.FirstName, employee.LastName, employee.PayPeriod, employee.SuperPercentage, employee.AnnualSalary);

                return Ok($"{paydetails.FullName},{paydetails.PayPeriod},{paydetails.GrossIncome},{paydetails.IncomeTax},{paydetails.NetIncome},{paydetails.Super}");
            }
            else
            {
                return BadRequest("Invalid Input");
            }
        }
        [HttpPost]
        public IActionResult GenerateBulkPayslip(IFormFile file)
        {
            if (file != null && FileHelper.GetContentType(file.FileName) == Constants.CSVFileExtension)
            {
              
                var memory = new MemoryStream();
                var path = Path.Combine(Directory.GetCurrentDirectory(),Constants.PayslipResponseFileName);
                using (var fileStream = new StreamWriter(path))
                using (StreamReader sr = new StreamReader(file.OpenReadStream()))               
                {
                    var Fulltext = sr.ReadToEnd().ToString();
                    var rows = Fulltext.Split('\n'); //split full file text into rows

                    foreach (var row in rows)
                    {
                        if (!String.IsNullOrEmpty(row))
                        {
                            var employee = ValidateInputGetEmployye(row.Trim());
                            if (employee != null)
                            {
                                var paydetails = _payslipService.GetPayslip(employee.FirstName, employee.LastName, employee.PayPeriod, employee.SuperPercentage, employee.AnnualSalary);
                                var response= $"{paydetails.FullName},{paydetails.PayPeriod},{paydetails.GrossIncome},{paydetails.IncomeTax},{paydetails.NetIncome},{paydetails.Super}";
                                fileStream.WriteLine(response);
                                fileStream.Flush();
                            }
                            else
                            {
                                return BadRequest("Invalid File Format");
                            }
                        }

                    }

                }
               
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Seek(0, SeekOrigin.Begin);
                System.IO.File.Delete(path);
                return File(memory, Constants.CSVMimeType, Constants.PayslipResponseFileName);
              
            }
            else
            {
                return BadRequest("Invalid File Format");
            }
        }        
        private Employee ValidateInputGetEmployye(string csvInput)
        {           

            if (!string.IsNullOrEmpty(csvInput) && csvInput.Trim().Split(',').Count() == 5)
            {
                var values = csvInput.Trim().Split(',');
                var employee = new Employee
                {
                    FirstName = values[0].Trim(),
                    LastName = values[1].Trim(),
                    AnnualSalary = Convert.ToInt32(values[2]),
                    SuperPercentage = Convert.ToDecimal(values[3].TrimEnd('%')),
                    PayPeriod = values[4]


                };
                return employee;
            }
            else
            {
                return null;
            }
        }
    }
}