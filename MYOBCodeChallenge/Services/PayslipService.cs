using MYOB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MYOBCodeChallenge.Utilities;

namespace MYOBCodeChallenge.Services
{
    public class PayslipService : IPayslipService
    {
        public Payslip GetPayslip(string firstName, string lastName,string payPeriod, decimal super, int annualSalary)
        {
            try
            {
                var grossSalary = ((decimal)annualSalary).DivideRoundOffToInt(12);
                var superAnnuation = CalculateSuper(grossSalary, super);
                var incomeTax = TaxCalculator.CalculateIncomeTax(annualSalary);
                var netIncome = grossSalary - incomeTax;
                var payslip = new Payslip
                {
                    FullName = firstName + " " + lastName,
                    GrossIncome = grossSalary,
                    IncomeTax = incomeTax,
                    NetIncome = netIncome,
                    PayPeriod = payPeriod,
                    Super = superAnnuation
                };
                return payslip;
            }
            catch (InvalidCastException ex)
            {
                //To-Do: log exception
                throw ex;
            }
            catch (Exception ex)
            {
                //To-DO: log exception
                throw ex;

            }

        }
        private int CalculateSuper(int grossSalary, decimal super)
        {
            return ((decimal)grossSalary * super).DivideRoundOffToInt(100);
        }
    }
}
