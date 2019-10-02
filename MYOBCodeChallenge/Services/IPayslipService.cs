using MYOB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYOBCodeChallenge.Services
{
    public interface IPayslipService
    {
        Payslip GetPayslip(string firstName, string lastName,string payPeriod, decimal super, int grossSalary);
    }
}
